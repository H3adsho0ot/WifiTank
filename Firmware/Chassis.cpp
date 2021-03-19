#ifndef Chassis_cpp
#define Chassis_cpp

#include "Arduino.h"
#include "Config.h"
#include "FastAccelStepper.h"

class Chassis
{
  private:
    FastAccelStepperEngine engine = FastAccelStepperEngine();
    FastAccelStepper *leftStepper = NULL;

  public:
    Chassis() //: leftStepper(stepsPerRevolution, leftDir, leftStep)
    {}

    void setup()
    {
      engine.init();
      leftStepper = engine.stepperConnectToPin(leftStep);
      
      if (leftStepper)
      {
        leftStepper->setDirectionPin(leftDir);
        leftStepper->setAutoEnable(true);
        leftStepper->setSpeedInUs(minSpeed);
        leftStepper->setAcceleration(accel);
      }
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
      leftChainSpeed = map(leftChainSpeed, 0, 100, minSpeed, maxSpeed);

      leftStepper->setAcceleration(accel);
      
      bool braking = false;

      int motorSpeed = leftStepper->getCurrentSpeedInUs();
      if(motorSpeed > 0 && !leftChainForward  && leftChainSpeed != minSpeed)
      {
        braking = true;
      }
      else    
      if(motorSpeed < 0 && leftChainForward && leftChainSpeed != minSpeed)
      {
        braking = true;
        motorSpeed = motorSpeed * -1;
      }

      if(braking)
      {
        leftChainSpeed = minSpeed;
        leftStepper->setAcceleration(decel);
      }
      
      if(motorSpeed == minSpeed && leftChainSpeed == minSpeed)
      {
          leftStepper->stopMove();
      }
      else if(!leftStepper->isMotorRunning() && leftChainSpeed < minSpeed)
      {        
        leftStepper->setSpeedInUs(accel);
        if(leftChainForward)
        {
          leftStepper->moveByAcceleration(accel, false);
        }
        else
        {
          leftStepper->moveByAcceleration(accel * -1, true);
        }
      }
      else 
      {
        leftStepper->setSpeedInUs(leftChainSpeed);
        leftStepper->applySpeedAcceleration();
      }
    }
};

#endif
