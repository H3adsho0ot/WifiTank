#ifndef Chassis_cpp
#define Chassis_cpp

#include "Arduino.h"
#include "Config.h"

#include "A4988.h"


class Chassis
{
  private:

    /*const int LEDC_CHANNEL_2 = 2;
    const int LEDC_CHANNEL_3 = 3;
    const int LEDC_CHANNEL_4 = 4;
    const int LEDC_CHANNEL_5 = 5;

    const int LEDC_TIMER_8_BIT = 8;
    const int LEDC_BASE_FREQ = 490;*/ 
    A4988 leftStepper;  

  public:
    Chassis() : leftStepper(stepsPerRevolution, leftDir, leftStep)
    {}

    void setup()
    {
      //leftStepper(stepsPerRevolution, leftDir, leftStep);
      /*pinMode(in1, OUTPUT);
      pinMode(in2, OUTPUT);
      pinMode(in3, OUTPUT);
      pinMode(in4, OUTPUT);

      ledcSetup(LEDC_CHANNEL_2, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in3, LEDC_CHANNEL_2);
      ledcSetup(LEDC_CHANNEL_3, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in4, LEDC_CHANNEL_3);
      ledcSetup(LEDC_CHANNEL_4, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in1, LEDC_CHANNEL_4);
      ledcSetup(LEDC_CHANNEL_5, LEDC_BASE_FREQ, LEDC_TIMER_8_BIT);
      ledcAttachPin(in2, LEDC_CHANNEL_5);*/
      
      leftStepper.setSpeedProfile(BasicStepperDriver::LINEAR_SPEED, accel, decel);
    }

    void drive(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
    {
      
      //map speed
      if (rightChainSpeed < 0)
      {
        rightChainSpeed = 0;
      }
      else if (rightChainSpeed > 100)
      {
        rightChainSpeed = 100;
      }

      if (leftChainSpeed < 0)
      {
        leftChainSpeed = 0;
      }
      else if (leftChainSpeed > 100)
      {
        leftChainSpeed = 100;
      }

      rightChainSpeed = map(rightChainSpeed, 0, 100, 0, 255);
      leftChainSpeed = map(leftChainSpeed, 0, 100, 0, maxRPM);

            
      leftStepper.begin(leftChainSpeed, 1);
      if (leftChainForward)
      {
        leftStepper.rotate(360);
      }
      

      //set speed & direction
      //right chain
      /*if (rightChainForward)
      {
        ledcWrite(LEDC_CHANNEL_4, rightChainSpeed);
        ledcWrite(LEDC_CHANNEL_5, 0);
      }
      else
      {
        ledcWrite(LEDC_CHANNEL_4, 0);
        ledcWrite(LEDC_CHANNEL_5, rightChainSpeed);
      }

      //left chain
      if (leftChainForward)
      {
        ledcWrite(LEDC_CHANNEL_2, 0);
        ledcWrite(LEDC_CHANNEL_3, leftChainSpeed);
      }
      else
      {
        ledcWrite(LEDC_CHANNEL_2, leftChainSpeed);
        ledcWrite(LEDC_CHANNEL_3, 0);
      }*/
    }
};

#endif
