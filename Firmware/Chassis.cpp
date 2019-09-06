#ifndef Chassis_cpp_
#define Chassis_cpp_

#include "Arduino.h"
#include "Config.h"

class Chassis
{
  public:
  Chassis()
  {}
  
  void setup()
  {
    pinMode(ENA, OUTPUT);    
    pinMode(ENB, OUTPUT);
    pinMode(in1, OUTPUT);
    pinMode(in2, OUTPUT);
    pinMode(in3, OUTPUT);
    pinMode(in4, OUTPUT);
  }
  
  void drive(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
  {
    //set directions
    //right chain
    if(leftChainForward)
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
    if(rightChainForward)
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
    rightChainSpeed = map(rightChainSpeed, 0, 100, 0, 255); 
    leftChainSpeed = map(leftChainSpeed, 0, 100, 0, 255);
  
    //set speed
    analogWrite(ENA, rightChainSpeed);
    analogWrite(ENB, leftChainSpeed);
  }
};

#endif
