#include <Wire.h>
#include <SeeedGrayOLED.h>
#include <avr/pgmspace.h>


void setup()
{
  Wire.begin();
  SeeedGrayOled.init(SSD1327);             //initialize SEEED OLED display
  SeeedGrayOled.clearDisplay();     //Clear Display.
  SeeedGrayOled.setNormalDisplay(); //Set Normal Display Mode
  SeeedGrayOled.setVerticalMode();  // Set to vertical mode for displaying text
  

  SeeedGrayOled.setTextXY(5,3);  //set Cursor to ith line, 0th column
  SeeedGrayOled.setGrayLevel(15); //Set Grayscale level. Any number between 0 - 15.
  SeeedGrayOled.putString("TROJKA"); //Print Hello World
  SeeedGrayOled.setTextXY(6,2);  //set Cursor to ith line, 0th column
  SeeedGrayOled.setGrayLevel(15); //Set Grayscale level. Any number between 0 - 15.
  SeeedGrayOled.putString("Software"); //Print Hello World

  SeeedGrayOled.setTextXY(8,4);  //set Cursor to ith line, 0th column
  SeeedGrayOled.setGrayLevel(10); //Set Grayscale level. Any number between 0 - 15.
  SeeedGrayOled.putString("goes"); //Print Hello World

  SeeedGrayOled.setTextXY(10,4);  //set Cursor to ith line, 0th column
  SeeedGrayOled.setGrayLevel(15); //Set Grayscale level. Any number between 0 - 15.
  SeeedGrayOled.putString("#IOT"); //Print Hello World

}

void loop()
{
  
}


