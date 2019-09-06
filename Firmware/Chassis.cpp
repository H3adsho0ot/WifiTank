#ifndef Chassis_cpp_
#define Chassis_cpp_

#include "Arduino.h"

class Chassis
{
  private:
  int _ENA;
  int _in1;  
  int _in2; 
  
  //chain left
  int _ENB;
  int _in3;
  int _in4;
  
  public:
  Chassis(int ENA, int in1, int in2, int in3, int in4, int ENB)
  {
    _ENA = ENA;
    _in1 = in1;
    _in2 = in2;
    _in3 = in3;
    _in4 = in4;
    _ENB = ENB;
  }
  
  void setup()
  {
    pinMode(_ENA, OUTPUT);    
    pinMode(_ENB, OUTPUT);
    pinMode(_in1, OUTPUT);
    pinMode(_in2, OUTPUT);
    pinMode(_in3, OUTPUT);
    pinMode(_in4, OUTPUT);
  }
  
  void drive(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
  {
    //set directions
    //right chain
    if(rightChainForward)
    {
      digitalWrite(_in1, HIGH);
      digitalWrite(_in2, LOW);
    }
    else
    {
      digitalWrite(_in1, LOW);
      digitalWrite(_in2, HIGH);
    }
  
    //left chain
    if(leftChainForward)
    {
      digitalWrite(_in3, LOW);
      digitalWrite(_in4, HIGH);
    }
    else
    {
      digitalWrite(_in3, HIGH);
      digitalWrite(_in4, LOW);
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
    analogWrite(_ENA, rightChainSpeed);
    analogWrite(_ENB, leftChainSpeed);
  }
};

#endif
