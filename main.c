#include <xc.h>
#include <main.h>

#define _XTAL_FREQ 1000000
#define OSC1MHZ 0x58
#define BYTESPERLINE 12
#define TRUE 1
#define FALSE 0
#define SPI_CLKIDLEHIGH 1
#define SPI_CLKIDLELOW 0
#define SPI_MASTER_FOSC4 0
#define SPI_MASTER_FOSC16 1
#define SPI_MASTER_FOSC64 2
#define SPI_SAMPLEATEND 0
#define SPI_SAMPLEINMID 1
#define SPI_IDLETOACTIVE 0
#define SPI_ACTIVETOIDLE 1
#define TOPPADDING 5
#define BIGDIGSHEIGHT 50
#define SMALLDIGSHEIGHT 30
#define MIDDLESPACER 8
#define NEGATIVESIGNSTARTLINE 30
#define NEGATIVESIGNSTOPLINE 34
#define DECIMALPOINTSTARTLINE 87
#define DEGREESCSIGNSTARTLINE 66
#define DEGREESCSIGNSTOPLINE 76
#define BUSYPIXELLINE 2
#define SLEEP1MS 0x01
#define SLEEP256MS 0x11
#define SLEEP1S 0x15
#define SLEEP4S 0x19
#define SLEEP8S 0x1B
#define SLEEP16S 0x1D
#define SLEEP32S 0x1F
#define SLEEP64S 0x21
#define SLEEP128S 0x23
#define SLEEP256S 0x25

int currentDigitsLeftOfDecimal;
int currentDigitsRightOfDecimal;
double currentTemp;
double previousTemps[4];

int main(void)
{
    currentDigitsLeftOfDecimal = 0;
    currentDigitsRightOfDecimal = 0;
    currentTemp = 0;
    previousTemps[0] = 0;
    previousTemps[1] = 0;
    previousTemps[2] = 0;
    previousTemps[3] = 0;

    initMicro();
    LCD_enableExternalOsc(TRUE);
    LCD_enable(TRUE);
    LCD_clear();
    LCD_testChars();

    SPI_configureForTS();
    TS_select(TRUE);
    TS_serialReset();                         
    TS_select(FALSE);

    while(1)
    {
        SPI_configureForLCD();
        LCD_writeBusyIndicatorPixels(TRUE);

        SPI_configureForTS();
        TS_select(TRUE);
        //TS_serialReset();                         //Uncomment this if there are long-term stability issues
        currentTemp = TS_measureTemp();
        TS_select(FALSE);

        SPI_configureForLCD();
        LCD_writeTemp(currentTemp);
        LCD_writeBusyIndicatorPixels(FALSE);
        
        sleepForTime(SLEEP8S);
    };
    return 0;
}

void SPI_enable(char state)
{
    if (state == FALSE)
        SSPCON1 = SSPCON1 & 0b11011111;
    else
        SSPCON1 = SSPCON1 | 0b00100000;
}

void SPI_setIdleState(char state)
{
    if (state == SPI_CLKIDLELOW)
        SSPCON1 = SSPCON1 & 0b11101111;
    else
        SSPCON1 = SSPCON1 | 0b00010000;
}

void SPI_setMode(char state)
{
    SSPCON1 = SSPCON1 & 0b11110000;
    switch (state)
    {
        case SPI_MASTER_FOSC4:
            break;
        case SPI_MASTER_FOSC16:
            SSPCON1 = SSPCON1 | 0b00000001;
            break;
        case SPI_MASTER_FOSC64:
            SSPCON1 = SSPCON1 | 0b00000010;
            break;
    }
}

void SPI_configureForTS(void)
{
    SPI_enable(FALSE);
    SPI_setDataSampleMode(SPI_SAMPLEATEND);
    SPI_setTransmitMode(SPI_IDLETOACTIVE);
    SPI_setIdleState(SPI_CLKIDLEHIGH);
    SPI_enable(TRUE);
}

void SPI_configureForLCD(void)
{
    SPI_enable(FALSE);
    SPI_setDataSampleMode(SPI_SAMPLEINMID);
    SPI_setTransmitMode(SPI_ACTIVETOIDLE);
    SPI_setIdleState(SPI_CLKIDLELOW);
    SPI_enable(TRUE);
}

void SPI_setDataSampleMode(char state)
{
    if (state == SPI_SAMPLEATEND)
        SSPSTAT = SSPSTAT | 0b10000000;
    else
        SSPSTAT = SSPSTAT & 0b01111111;
}

void SPI_setTransmitMode(char state)
{
    if (state == SPI_IDLETOACTIVE)
        SSPSTAT = SSPSTAT & 0b10111111;
    else
        SSPSTAT = SSPSTAT | 0b01000000;
}

void SPI_writeByte(unsigned char byte)
{
    SSPCON1bits.WCOL = 0;			//Clear any previous write collision
    SSPBUF = byte;                              //write byte to SSPBUF register
    __delay_us(50);
    byte = SSPBUF;                              //Read the data that came in (recommended by Microchip)
}

unsigned char SPI_readByte(void)
{
    //unsigned char byte;
    while (SSPSTATbits.BF);                     //wait until receive buffer empties
    SSPBUF = 0xFF;                              //write dummy data to initiate cycle
    //while (!SSPSTATbits.BF);                  //wait until receive buffer fills and cycle completes
    __delay_us(100);
    //byte = SSPBUF;                              //read the received data from the buffer
    return SSPBUF;                                //return with byte read
}

void LCD_enable(char state)
{
    if (state == FALSE)
        LATB = LATB & 0b11111101;
    else
        LATB = LATB | 0b00000010;
}

void LCD_select(char state)
{
    if (state == FALSE)
        LATB = LATB & 0b11111011;             //CS is active high
    else
        LATB = LATB | 0b00000100;
}

void LCD_enableExternalOsc(char state)
{
    if (state == FALSE)
        LATB = LATB & 0b11101111;
    else
        LATB = LATB | 0b00010000;
}

void LCD_clear(void)
{
    LCD_select(TRUE);
    SPI_writeByte(0b10100000);                  //Data update mode, don't care frame inversion flag, all clear
    SPI_writeByte(0x00);                        //Dummy data
    LCD_select(FALSE);
    __delay_ms(1);
}

void LCD_writeLine(char data[], unsigned char line)
{
    LCD_select(TRUE);
    SPI_writeByte(0b10000000);                  //Data update mode, don't care frame inversion flag, don't do all clear
    line = reverseByte(line);
    SPI_writeByte(line);
    int byteNum;
    for (byteNum = 0; byteNum < BYTESPERLINE; byteNum++)
    {
        SPI_writeByte(data[byteNum]);
    }
    SPI_writeByte(0x00);                        //Dummy data 1
    SPI_writeByte(0x00);                        //Dummy data 2
    LCD_select(FALSE);
}

void LCD_writeMultiLine(char data[], int startLine, int endLine)
{
    LCD_select(TRUE);
    SPI_writeByte(0b10000000);                  //Data update mode, don't care frame inversion flag, don't do all clear
    int byteNum;
    int lineNum;
    int byteIndex = 0;
    for (lineNum = startLine; lineNum <= endLine; lineNum++)
    {
        SPI_writeByte(reverseByte(lineNum));
        for (byteNum = 0x00; byteNum < BYTESPERLINE; byteNum++)
        {
            SPI_writeByte(data[byteIndex]);
            ++byteIndex;
        }
        SPI_writeByte(0x00);
    }
    SPI_writeByte(0x00);
    LCD_select(FALSE);
}

void LCD_writeTopBig(char firstChar[], char secondChar[], int isNegative)
{
    LCD_select(TRUE);
    SPI_writeByte(0b10000000);                  //Data update mode, don't care frame inversion flag, don't do all clear
    int lineNum;
    int firstCharIndex = 0;
    int secondCharIndex = 0;
    for (lineNum = TOPPADDING; lineNum < TOPPADDING + BIGDIGSHEIGHT; lineNum++)
    {
        SPI_writeByte(reverseByte(lineNum));
        if (isNegative == TRUE && lineNum >= NEGATIVESIGNSTARTLINE && lineNum <= NEGATIVESIGNSTOPLINE)
        {
            SPI_writeByte(0xF0);
            SPI_writeByte(0x0F);
        }
        else
        {
            SPI_writeByte(0xFF);
            SPI_writeByte(0xFF);
        }
        SPI_writeByte(compressionTable[firstChar[firstCharIndex]][0]);
        SPI_writeByte(compressionTable[firstChar[firstCharIndex]][1]);
        ++firstCharIndex;
        SPI_writeByte(compressionTable[firstChar[firstCharIndex]][0]);
        SPI_writeByte(compressionTable[firstChar[firstCharIndex]][1]);
        ++firstCharIndex;
        SPI_writeByte(0xFF);
        SPI_writeByte(compressionTable[secondChar[secondCharIndex]][0]);
        SPI_writeByte(compressionTable[secondChar[secondCharIndex]][1]);
        ++secondCharIndex;
        SPI_writeByte(compressionTable[secondChar[secondCharIndex]][0]);
        SPI_writeByte(compressionTable[secondChar[secondCharIndex]][1]);
        ++secondCharIndex;
        SPI_writeByte(0xFF);
        SPI_writeByte(0x00);
    }
    SPI_writeByte(0x00);
    LCD_select(FALSE);
}

void LCD_writeBottomSmall(char firstChar[], char secondChar[], char thirdChar[])
{
    LCD_select(TRUE);
    SPI_writeByte(0b10000000);                  //Data update mode, don't care frame inversion flag, don't do all clear
    int lineNum;
    int decimalPointIndex = 0;
    int firstCharIndex = 0;
    int secondCharIndex = 0;
    int thirdCharIndex = 0;
    int degreesCIndex = 0;
    int isOddLine = 0;
    for (lineNum = TOPPADDING + BIGDIGSHEIGHT + MIDDLESPACER; lineNum < TOPPADDING + BIGDIGSHEIGHT + MIDDLESPACER + SMALLDIGSHEIGHT; lineNum++)
    {
        SPI_writeByte(reverseByte(lineNum));
        SPI_writeByte(0xFF);
        
        if (lineNum >= DECIMALPOINTSTARTLINE)
        {
            SPI_writeByte(decimalPoint[decimalPointIndex]);
            decimalPointIndex++;
        }
        else
        { SPI_writeByte(0xFF); }
        

        if (isOddLine == 0)
        {
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][0]);
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][1]);
            firstCharIndex++;
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][0]);
        }
        else
        {
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][1]);
            firstCharIndex++;
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][0]);
            SPI_writeByte(compressionTable[firstChar[firstCharIndex]][1]);
            firstCharIndex++;
        }
        
        if (isOddLine == 0)
        {
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][0]);
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][1]);
            secondCharIndex++;
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][0]);
        }
        else
        {
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][1]);
            secondCharIndex++;
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][0]);
            SPI_writeByte(compressionTable[secondChar[secondCharIndex]][1]);
            secondCharIndex++;
        }

        if (isOddLine == 0)
        {
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][0]);
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][1]);
            thirdCharIndex++;
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][0]);
        }
        else
        {
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][1]);
            thirdCharIndex++;
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][0]);
            SPI_writeByte(compressionTable[thirdChar[thirdCharIndex]][1]);
            thirdCharIndex++;
        }

        if (lineNum > DEGREESCSIGNSTARTLINE && lineNum <= DEGREESCSIGNSTOPLINE)
        {
            SPI_writeByte(degreesCSign[degreesCIndex]);
            degreesCIndex++;
        }
        else
        { SPI_writeByte(0xFF); }
        
        SPI_writeByte(0x00);
        isOddLine = !isOddLine;
    }
    SPI_writeByte(0x00);
    LCD_select(FALSE);
}

void LCD_writeBusyIndicatorPixels(int state)
{
    LCD_select(TRUE);
    SPI_writeByte(0b10000000);                  //Data update mode, don't care frame inversion flag, don't do all clear
    SPI_writeByte(reverseByte(BUSYPIXELLINE));
    int byteNum;
    if (state)
    { SPI_writeByte(0xAB); }
    else
    { SPI_writeByte(0xFF); }
    for (byteNum = 1; byteNum < BYTESPERLINE; byteNum++)
    {
        SPI_writeByte(0xFF);
    }
    SPI_writeByte(0x00);                        //Dummy data 1
    SPI_writeByte(0x00);                        //Dummy data 2
    LCD_select(FALSE);
}

void LCD_writeTemp(double temperature)
{
    double averageTemp = 0;
    int digits[5] = {0,0,0,0,0};

    averageTemp = (temperature + previousTemps[0] + previousTemps[1] + previousTemps[2] + previousTemps[3]) / 5.0;
    previousTemps[3] = previousTemps[2];
    previousTemps[2] = previousTemps[1];
    previousTemps[1] = previousTemps[0];
    previousTemps[0] = temperature;

    convertDoubleToIntArray(averageTemp, digits);

    /*DEBUG - TRY TO FIGURE OUT IF ARRAY INDEXES ARE OUT OF BOUNDS*/
    char data[12] = {0x81,0x81,0x81,0x81,0x81,0x80,0x00,0x00,0x00,0x00,0x00,0x00};
    if ((digits[0] > 9))
    {
        data[0] = 0xE7;
        LCD_writeLine(data, 0x5E);
    }
    else if ((digits[1] > 9))
    {
        data[1] = 0xE7;
        LCD_writeLine(data, 0x5E);
    }
    else if ((digits[2] > 9))
    {
        data[2] = 0xE7;
        LCD_writeLine(data, 0x5E);
    }
    else if ((digits[3] > 9))
    {
        data[3] = 0xE7;
        LCD_writeLine(data, 0x5E);
    }
    else if ((digits[4] > 9))
    {
        data[4] = 0xE7;
        LCD_writeLine(data, 0x5E);
    }

    if ((digits[0]) < 0)
    {
        data[0] = 0xE7;
        LCD_writeLine(data, 0x5F);
    }
    else if ((digits[1]) < 0)
    {
        data[1] = 0xE7;
        LCD_writeLine(data, 0x5F);
    }
    else if ((digits[2]) < 0)
    {
        data[2] = 0xE7;
        LCD_writeLine(data, 0x5F);
    }
    else if ((digits[3]) < 0)
    {
        data[3] = 0xE7;
        LCD_writeLine(data, 0x5F);
    }
    else if ((digits[4]) < 0)
    {
        data[4] = 0xE7;
        LCD_writeLine(data, 0x5F);
    }
    /*DEBUG - TRY TO FIGURE OUT IF ARRAY INDEXES ARE OUT OF BOUNDS*/

    int digitsLeftOfDecimal = (digits[0] * 10) + digits[1];
    int digitsRightOfDecimal = (digits[2] * 100) + (digits[3] * 10) + digits[4];

    if (digitsLeftOfDecimal != currentDigitsLeftOfDecimal)
    {
        if (averageTemp >= 0.0)
        { LCD_writeTopBig(bigNumbers[digits[0]],bigNumbers[digits[1]], FALSE); }
        else if (averageTemp < 0.0)
        { LCD_writeTopBig(bigNumbers[digits[0]],bigNumbers[digits[1]], TRUE); }
    }
    if (digitsRightOfDecimal != currentDigitsRightOfDecimal)
    { LCD_writeBottomSmall(smallNumbers[digits[2]],smallNumbers[digits[3]], smallNumbers[digits[4]]); }

    currentDigitsLeftOfDecimal = digitsLeftOfDecimal;
    currentDigitsRightOfDecimal = digitsRightOfDecimal;
}

void LCD_testChars(void)
{
    int testDigit = 0;
    while (testDigit < 10)
    {
        LCD_writeTopBig(bigNumbers[testDigit],bigNumbers[testDigit], TRUE);
        LCD_writeBottomSmall(smallNumbers[testDigit],smallNumbers[testDigit], smallNumbers[testDigit]);
        testDigit++;
    }
}

void TS_select(char state)
{
    if (state == FALSE)
        LATB = LATB | 0b00001000;             //CS is active low
    else
        LATB = LATB & 0b11110111;
}

void TS_serialReset(void)
{
    SPI_writeByte(0xFF);
    SPI_writeByte(0xFF);
    SPI_writeByte(0xFF);
    SPI_writeByte(0xFF);
    __delay_us(600);
}

double TS_measureTemp(void)
{
    int leftByte = 0;
    int rightByte = 0;
    int twosComplementInt = 0;
    double temperature = 0;

    SPI_writeByte(0b00001000);                  //Tell the TS we want to write to the configuration register
    SPI_writeByte(0b10100000);                  //Configure TS for 16-bit resolution, and perform conversion via one-shot mode
    sleepForTime(SLEEP256MS);                   //It takes 240ms to perform a conversion; 256ms is the closest the WDT can do

    SPI_writeByte(0b01010000);                //Tell the TS we want to read the temp register (addr 0x02)
    leftByte = SPI_readByte();
    rightByte = SPI_readByte();

    leftByte = leftByte << 8;
    twosComplementInt = leftByte | rightByte;
    if ((twosComplementInt & 0x8000) == 0x8000)         //If bit 15 is set, the number is negative (twos complement representation)
    { twosComplementInt = twosComplementInt - 65536; }  //Formula taken from ADT7310 datasheet

    temperature = twosComplementInt / 128.0;            //Factor of 128 is from the datasheet; used for 16 bit measurements

    return temperature;
}

void sleepForTime(char time)
{
    WDTCON = time;                          //Configure the WDT for time and start counting
    SLEEP();                                //Enter sleep state
    WDTCON = 0x00;                          //Disable the WDT after waking
}

void initMicro(void)
{
    /*Master clock set-up*/
    OSCCON = OSC1MHZ;                              //Set internal oscillator to 1MHz

    //Set all RAx pins as outputs and drive low (none are used)
    TRISA = 0x00;                               //Set all PORTA pins as outputs
    PORTA = 0x00;                               //Set all PORTA pins to drive low

    //Set all unused RBx pins as outputs and drive low
    TRISB = TRISB & 0b00011110;                 //Set unused PORTB pins as outputs
    PORTB = PORTB & 0b00011110;                 //Set unused PORTB pins to drive low

    //Set all unused RCx pins as outputs and drive low
    TRISC = TRISC & 0b00011110;                 //Set unused PORTC pins as outputs
    PORTC = PORTC & 0b00011110;                 //Set unused PORTC pins to drive low

    //Disable the Fixed Voltage Reference
    FVRCONbits.FVREN = 0;

    //Disable the ADC module
    ADCON0bits.ADON = 0;

    //Disable the Op Amp modules
    OPA1CONbits.OPA1EN = 0;
    OPA2CONbits.OPA2EN = 0;

    //Disable the 8-bit DAC
    DAC1CON0bits.DAC1EN = 0;

    //Disable the 5-bit DACs
    DAC2CON0bits.DAC2EN = 0;
    DAC2CON0bits.DAC2OE1 = 0;
    DAC2CON0bits.DAC2OE2 = 0;
    DAC3CON0bits.DAC3EN = 0;
    DAC3CON0bits.DAC3OE1 = 0;
    DAC3CON0bits.DAC3OE2 = 0;
    DAC4CON0bits.DAC4EN = 0;
    DAC4CON0bits.DAC4OE1 = 0;
    DAC4CON0bits.DAC4OE2 = 0;

    //Disable the comparator module
    CM1CON0bits.C1ON = 0;
    CM1CON0bits.C1OE = 0;
    CM2CON0bits.C2ON = 0;
    CM2CON0bits.C2OE = 0;
    CM3CON0bits.C3ON = 0;
    CM3CON0bits.C3OE = 0;

    //Disable Timer1 module
    T1CONbits.TMR1ON = 0;

    //Disable Timer2 module
    T2CONbits.TMR2ON = 0;

    //Disable Programmable Switch Mode Controllers
    PSMC1CONbits.PSMC1EN = 0;
    PSMC1OEN = 0x00;                        //Turn off PWM outputs on all pins
    PSMC1STR0 = 0x00;                       //Make all PWM outputs inactive
    PSMC2CONbits.PSMC2EN = 0;
    PSMC2OEN = 0x00;                        //Turn off PWM outputs on all pins
    PSMC2STR0 = 0x00;                       //Make all PWM outputs inactive
    PSMC3CONbits.PSMC3EN = 0;
    PSMC3OEN = 0x00;                        //Turn off PWM outputs on all pins
    PSMC3STR0 = 0x00;                       //Make all PWM outputs inactive
    PSMC4CONbits.PSMC4EN = 0;
    PSMC4OEN = 0x00;                        //Turn off PWM outputs on all pins
    PSMC4STR0 = 0x00;                       //Make all PWM outputs inactive

    //Disable Capture/Compare/PWM Modules
    CCP1CON = 0x00;
    CCP2CON = 0x00;
    CCP3CON = 0x00;

    //Disable the EUSART module
    TXSTAbits.TXEN = 0;
    RCSTAbits.SPEN = 0;
    RCSTAbits.SREN = 0;
    RCSTAbits.CREN = 0;

    /*SPI communication set-up*/
    SPI_enable(FALSE);
    SPI_setMode(SPI_MASTER_FOSC4);
    SPI_setDataSampleMode(SPI_SAMPLEINMID);
    SPI_setTransmitMode(SPI_ACTIVETOIDLE);
    SPI_setIdleState(SPI_CLKIDLELOW);
    SPI_enable(TRUE);

    /*Set up IO pins*/
    // DISPLAY_EN for LCD
    LCD_enable(FALSE);                           //Disable LCD display
    TRISBbits.TRISB1 = 0;                       //Set RB1 as an output
    //OSC_EXTCOM_EN
    LCD_enableExternalOsc(FALSE);                   //Disable external oscillatorLCD
    TRISBbits.TRISB4 = 0;                       //Set RB4 as output
    // SPI CS for LCD
    LCD_select(FALSE);                           //De-select LCD (CS is active high)
    TRISBbits.TRISB2 = 0;                       //Set RB2 as an output
    // SPI MOSI
    PORTCbits.RC5 = 0;                          //Set MOSI low
    APFCON1bits.SDOSEL = 0;                     //Set SDO to be on RC5
    TRISCbits.TRISC5 = 0;                       //Set RC5 as an output
    // SPI SCLK
    PORTCbits.RC3 = 0;                          //Set SCLK low
    APFCON1bits.SCKSEL = 0;                     //Set SCK to be on RC3
    TRISCbits.TRISC3 = 0;                       //Set RC3 as an output
    // SPI MISO
    PORTCbits.RC4 = 0;                          //Set MISO low
    APFCON1bits.SDISEL = 0;                     //Set SDI to be on RC4
    TRISCbits.TRISC4 = 1;                       //Set RC4 as an input
    // SPI CS for temp sensor
    TS_select(FALSE);                            //Set CS high to not select temp sensor
    TRISBbits.TRISB3 = 0;                       //Set RB3 as an output
}

unsigned char reverseByte(unsigned char byte)
{
   //The LCD expects data LSB first, so this function must be used to prior to sending it
   //The algorithm was taken from a website, not sure how it works
   byte = (byte & 0xF0) >> 4 | (byte & 0x0F) << 4;
   byte = (byte & 0xCC) >> 2 | (byte & 0x33) << 2;
   byte = (byte & 0xAA) >> 1 | (byte & 0x55) << 1;
   return byte;
}

void convertDoubleToIntArray(double input, int digits[])
{
    //Takes a double and extracts the digits into an array of ints
    //Assumes that 
    if (input < 0.0)
    {
        input = input * -1.0;
    }
    digits[0] = (int)(input / 10.0);
    digits[1] = (int)(input - ((double)digits[0] * 10.0));
    digits[2] = (int)((input - (((double)digits[0] * 10.0) + (double)digits[1])) * 10.0);
    digits[3] = (int)((input - (((double)digits[0] * 10.0) + (double)digits[1] + ((double)digits[2] / 10.0))) * 100.0);
    digits[4] = (int)((input - (((double)digits[0] * 10.0) + (double)digits[1] + ((double)digits[2] / 10.0) + ((double)digits[3] / 100.0))) * 1000.0);
}

