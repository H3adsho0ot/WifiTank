#ifndef Chassis_cpp_
#define Chassis_cpp_

#include "Arduino.h"
#include "Config.h"

class Chassis
{
  private:
    const int LEDC_CHANNEL_0 = 0;
    const int LEDC_CHANNEL_1 = 1;
    const int LEDC_CHANNEL_2 = 2;
    const int LEDC_CHANNEL_3 = 3;

    const int LEDC_TIMER_8_BIT = 8;
    const int LEDC_BASE_FREQ = 490;
    
  public:
    Chassis()
    {}
    
    void setup()
    {
      pinMode(in1, OUTPUT);
      pinMode(in2, OUTPUT);
      pinMode(in3, OUTPUT);
      pinMode(in4, OUTPUT);
      
      ledcSetup(LEDC_CHANNEL_0, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in1, LEDC_CHANNEL_0);
      ledcSetup(LEDC_CHANNEL_1, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in2, LEDC_CHANNEL_1);
      ledcSetup(LEDC_CHANNEL_2, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in3, LEDC_CHANNEL_2);
      ledcSetup(LEDC_CHANNEL_3, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in4, LEDC_CHANNEL_3);
    }
  
    void drive(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
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

      //set speed & direction
      //right chain
      if(rightChainForward)
      {
        ledcWrite(LEDC_CHANNEL_0, rightChainSpeed);
        ledcWrite(LEDC_CHANNEL_1, 0);
      }
      else
      {
        ledcWrite(LEDC_CHANNEL_0, 0);
        ledcWrite(LEDC_CHANNEL_1, rightChainSpeed);
      }
    
      //left chain
      if(leftChainForward)
      {
        ledcWrite(LEDC_CHANNEL_2, 0);
        ledcWrite(LEDC_CHANNEL_3, leftChainSpeed);
      }
      else
      {
        ledcWrite(LEDC_CHANNEL_2, leftChainSpeed);
        ledcWrite(LEDC_CHANNEL_3, 0);
      }
    }
};

#endif
