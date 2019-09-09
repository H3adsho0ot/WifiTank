#ifndef Chassis_cpp_
#define Chassis_cpp_

#include "Arduino.h"
#include "Config.h"

class Chassis
{
  private:
    #define LEDC_CHANNEL_0     0
    #define LEDC_CHANNEL_1     1
    #define LEDC_CHANNEL_2     2
    #define LEDC_CHANNEL_3     3
    #define LEDC_CHANNEL_led     4
    
    //use 13 bit precission for LEDC timer
    #define LEDC_TIMER_13_BIT  13
    //use 5000 Hz as a LEDC base frequency
    #define LEDC_BASE_FREQ     5000

    void ledcAnalogWrite(uint8_t channel, uint32_t value, uint32_t valueMax = 255) 
    {
      // calculate duty, 8191 from 2 ^ 13 - 1
      uint32_t duty = (8191 / valueMax) * min(value, valueMax);    
      // write duty to LEDC
      ledcWrite(channel, duty);
    }
  public:
    Chassis()
    {}
    
    void setup()
    {
      //pinMode(_ENA, OUTPUT);    
      //pinMode(_ENB, OUTPUT);
      //pinMode(in1, OUTPUT);
      //pinMode(in2, OUTPUT);
      //pinMode(in3, OUTPUT);
      //pinMode(in4, OUTPUT);
      
      ledcSetup(LEDC_CHANNEL_0, LEDC_BASE_FREQ, LEDC_TIMER_13_BIT);
      ledcAttachPin(in1, LEDC_CHANNEL_0);
      ledcSetup(LEDC_CHANNEL_1, LEDC_BASE_FREQ, LEDC_TIMER_13_BIT);
      ledcAttachPin(in2, LEDC_CHANNEL_1);
      ledcSetup(LEDC_CHANNEL_2, LEDC_BASE_FREQ, LEDC_TIMER_13_BIT);
      ledcAttachPin(in3, LEDC_CHANNEL_2);
      ledcSetup(LEDC_CHANNEL_3, LEDC_BASE_FREQ, LEDC_TIMER_13_BIT);
      ledcAttachPin(in4, LEDC_CHANNEL_3);

      ledcSetup(LEDC_CHANNEL_led, LEDC_BASE_FREQ, LEDC_TIMER_13_BIT);
      ledcAttachPin(4, LEDC_CHANNEL_led);
    }
    
    void driveL298N(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
    {
      //set directions
      //right chain
      if(rightChainForward)
      {
        digitalWrite(in1, HIGH);
        digitalWrite(in2, LOW);
      }
      else
      {
        digitalWrite(in1, LOW);
        digitalWrite(in2, HIGH);
      }
    
      //left chain
      if(leftChainForward)
      {
        digitalWrite(in3, LOW);
        digitalWrite(in4, HIGH);
      }
      else
      {
        digitalWrite(in3, HIGH);
        digitalWrite(in4, LOW);
      }
    
      //map speed
      if(rightChainSpeed < 0)
      {
        rightChainSpeed = 0;
      }
      else if(rightChainSpeed > 100)
      {
        rightChainSpeed = 100;
      }
  
      if(leftChainSpeed < 0)
      {
        leftChainSpeed = 0;
      }
      else if(leftChainSpeed > 100)
      {
        leftChainSpeed = 100;
      }
      
      rightChainSpeed = map(rightChainSpeed, 0, 100, 0, 255); 
      leftChainSpeed = map(leftChainSpeed, 0, 100, 0, 255);
    
      //set speed
      //digitalWrite(_ENA, HIGH);
      //digitalWrite(_ENB, HIGH);
    }
  
    void driveDRV8833(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
    {  
      //map speed
      if(rightChainSpeed < 0)
      {
        rightChainSpeed = 0;
      }
      else if(rightChainSpeed > 100)
      {
        rightChainSpeed = 100;
      }
  
      if(leftChainSpeed < 0)
      {
        leftChainSpeed = 0;
      }
      else if(leftChainSpeed > 100)
      {
        leftChainSpeed = 100;
      }
      
      rightChainSpeed = map(rightChainSpeed, 0, 100, 0, 255); 
      leftChainSpeed = map(leftChainSpeed, 0, 100, 0, 255);

      ledcAnalogWrite(LEDC_CHANNEL_led, rightChainSpeed);    
      //set speed & direction
      //right chain
      if(rightChainForward)
      {
        ledcAnalogWrite(LEDC_CHANNEL_0, rightChainSpeed);
        digitalWrite(in2, LOW);
      }
      else
      {
        digitalWrite(in1, LOW);
        ledcAnalogWrite(LEDC_CHANNEL_1, rightChainSpeed);
      }
    
      //left chain
      if(leftChainForward)
      {
        digitalWrite(in3, LOW);
        ledcAnalogWrite(LEDC_CHANNEL_3, leftChainSpeed);
      }
      else
      {
        ledcAnalogWrite(LEDC_CHANNEL_2, leftChainSpeed);
        digitalWrite(in4, LOW);
      }
    }
};

#endif
