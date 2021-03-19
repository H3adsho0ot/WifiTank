#ifndef Chassis_cpp
#define Chassis_cpp

#include "Arduino.h"
#include "Config.h"
#include "FastAccelStepper.h"

class Chassis
{
  private:
    FastAccelStepperEngine engine = FastAccelStepperEngine();
    FastAccelStepper *rightStepper = NULL;
    FastAccelStepper *leftStepper = NULL;    

  public:
    Chassis()
    {}

    void setup()
    {
      engine.init();
      
      rightStepper = engine.stepperConnectToPin(rightStep);
      leftStepper = engine.stepperConnectToPin(leftStep);     

      if (rightStepper)
      {
        rightStepper->setDirectionPin(rightDir);
        rightStepper->setAutoEnable(true);
        rightStepper->setSpeedInUs(minSpeed);
        rightStepper->setAcceleration(accel);
      }
      
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

      rightChainSpeed = map(rightChainSpeed, 0, 100, minSpeed, maxSpeed);
      leftChainSpeed = map(leftChainSpeed, 0, 100, minSpeed, maxSpeed);

      //Right Chain
      rightStepper->setAcceleration(accel);
      
      bool rightBraking = false;
      int rightMotorSpeed = rightStepper->getCurrentSpeedInUs();
      
      if(rightMotorSpeed > 0 && !rightChainForward  && rightChainSpeed != minSpeed)
      {
        rightBraking = true;
      }
      else    
      if(rightMotorSpeed < 0 && rightChainForward && rightChainSpeed != minSpeed)
      {
        rightBraking = true;
        rightMotorSpeed = rightMotorSpeed * -1;
      }

      if(rightBraking)
      {
        rightChainSpeed = minSpeed;
        rightStepper->setAcceleration(decel);
      }
   
      if(rightMotorSpeed == minSpeed && rightChainSpeed == minSpeed)
      {
          rightStepper->stopMove();
      }
      else if(!rightStepper->isMotorRunning() && rightChainSpeed < minSpeed)
      {        
        rightStepper->setSpeedInUs(accel);
        if(rightChainForward)
        {
          rightStepper->moveByAcceleration(accel, false);
        }
        else
        {
          rightStepper->moveByAcceleration(accel * -1, true);
        }
      }
      else 
      {
        rightStepper->setSpeedInUs(rightChainSpeed);
        rightStepper->applySpeedAcceleration();
      }

      //Left chain
      leftStepper->setAcceleration(accel);
      
      bool leftBraking = false;
      int leftMotorSpeed = leftStepper->getCurrentSpeedInUs();
      
      if(leftMotorSpeed > 0 && !leftChainForward  && leftChainSpeed != minSpeed)
      {
        leftBraking = true;
      }
      else    
      if(leftMotorSpeed < 0 && leftChainForward && leftChainSpeed != minSpeed)
      {
        leftBraking = true;
        leftMotorSpeed = leftMotorSpeed * -1;
      }

      if(leftBraking)
      {
        leftChainSpeed = minSpeed;
        leftStepper->setAcceleration(decel);
      }
   
      if(leftMotorSpeed == minSpeed && leftChainSpeed == minSpeed)
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
