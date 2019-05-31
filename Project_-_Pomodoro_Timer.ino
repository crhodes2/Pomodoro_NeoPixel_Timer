/*============================================================================
                              Adafruit Pomodoro Timer
        A 60min timer that decrement by minute simply by changing colors.



  Displays a countdown timer on the NeoPixels and sounds an alarm.

 How it Works:
  The time to countdown is specified by 6000 milliseconds (total_time).
  That's approximately 1 minute. And at each minute decrement, a Neopixel 
  light changes color, or simply turn itself off depending on how the timer
  was set.

  Example: If timer was set to 40 minutes, the color pink would 
  fill all the neopixel lights available. When 2 minutes have passed
  from 40 minutes (ie. 38 minutes left on the clock) two of the 
  pink neopixel light would change from their initial color to
  yellow, the next Neopixel color that represents the next 10 minute interval
  of 30 minutes.

 NeoPixel Colors:
  * Purple Rain : 60 mins left
  * Ruby Red: 50 mins left
  * Hot Pink: 40 mins left
  * Sun Gold: 30 mins left
  * Green Jade: 20 mins left
  * Light Blue: 10 mins left

 Code Challenges:
  * Add music at the end of timer (check, though not via C#)
  * Change orb color by user preference via C# code 
=============================================================================*/

#include <Adafruit_CircuitPlayground.h>
#include "pitches.h"
#include <Wire.h>
#include <SPI.h>

#define NUM 7


#define DIAMOND 0xB9F2FF
#define BLUE 0x008080     // 10 to 20 mins
#define GREEN 0x33FF33    // 20 to 30 mins
#define GOLD 0xFFD700     // 30 to 40 mins
#define PINK 0xFF2F2F     // 40 to 50 mins
#define RED 0xFF0101      // 50 to 60 mins
#define PURPLE 0x4B0082   // 60 to 70 mins

int melody[] = {
NOTE_E7, NOTE_E7, 0, NOTE_E7,
  0, NOTE_C7, NOTE_E7, 0,
  NOTE_G7, 0, 0,  0,
  NOTE_G6, 0, 0, 0
};

int numNotes;

int noteDurations[] = {     // note durations
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
 
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
 
  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
 
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
 
  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
};

float total_time = 60000; // approx 1 min per pixel light
float delta_time = total_time / 500; // use for debugging purposes
bool slideSwitch = CircuitPlayground.slideSwitch();
uint8_t currentColor;
const uint32_t colors[] = { DIAMOND, BLUE, GREEN, GOLD, PINK, RED, PURPLE };
bool is4Pressed = false;
bool is19Pressed = false;

void setup() 
{
  CircuitPlayground.begin();
  Serial.begin(115200);
  pinMode(21, INPUT);
  CircuitPlayground.setBrightness(125);
  currentColor = 0;
  numNotes = sizeof(melody)/sizeof(int);
}

void superMarioTone()
{
  for (int thisNote = 0; thisNote < numNotes; thisNote++) 
  { 
    int noteDuration = 800 / noteDurations[thisNote];
    CircuitPlayground.playTone(melody[thisNote], noteDuration);

    int pauseBetweenNotes = noteDuration * 1.30;
    delay(pauseBetweenNotes);
  }
}

void timer()
{
  //int myNum = NUM - 1;
  if(CircuitPlayground.slideSwitch() == true)
  {
    while(currentColor > 1)
    {
      currentColor = currentColor - 1;
      for (int p=0; p<10; p=p+1) 
      {
        delay(total_time);
        CircuitPlayground.setPixelColor(p, colors[currentColor]);  // 50 to 60 mins
      }
    }
  
    for (int p=0; p<10; p=p+1) 
    {
      delay(total_time);
      CircuitPlayground.setPixelColor(p, 0, 0, 0);  // 0 to 10 mins
    }
  
    while (true) {
      superMarioTone();
      //CircuitPlayground.playTone(800, 250);
      delay(250);
      superMarioTone();
      //CircuitPlayground.playTone(1500, 250);
      delay(250);
      superMarioTone();
      //CircuitPlayground.playTone(1000, 250);
      delay(250);   
    }
  }
  else
  {
    
  }
  
}

void rightButton()
{
  if(!is19Pressed)
  {
    if(digitalRead(19) == HIGH)
    {
      is19Pressed = true;

      if(currentColor != NUM - 1)
      {
        currentColor++;
      }
      else
      {
        CircuitPlayground.playTone(800, 250);
        currentColor = NUM - 1;
      }
    }
  }
  else
  {
    if(digitalRead(19) == LOW)
    {
      is19Pressed = false;
    }
  }

}

void leftButton()
{
  if(!is4Pressed)
  {
    if(digitalRead(4) == HIGH)
    {
      is4Pressed = true;
      if (currentColor == 0)
      {
        CircuitPlayground.playTone(400, 250);
        CircuitPlayground.playTone(400, 250);
        currentColor = 0;
      }
      else if (currentColor == 1)
      {
        CircuitPlayground.playTone(800, 250);
        currentColor = 1; 
      }
      else
      {  
        currentColor--;
      }
    }
  }
  
  else
  {
    if(digitalRead(4) == LOW)
    {
      is4Pressed = false;
    }
  }
}

void loop()
{
  for (int p=0; p<10; p=p+1) 
  {
    CircuitPlayground.setPixelColor(p, colors[currentColor]);
  }
  leftButton();
  rightButton();
  timer();

}
