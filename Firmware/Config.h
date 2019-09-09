#ifndef Config_h
#define Config_h
  //chassis
  //L298N
  //chain right
  //const int ENA = 9;    //digitalPin + PWM
  //const int in1 = 8;    //digitalPin
  //const int in2 = 10;   //digitalPin
  
  //chain left
  //const int ENB = 11;   //digitalPin + PWM
  //const int in3 = 13;   //digitalPin
  //const int in4 = 12;   //digitalPin

  //drv8833
  //chain right
  //const int ENA = 0;
  //const int ENB = 0;
  
  const int in1 = 12;   //digitalPin + PWM 
  const int in2 = 13;   //digitalPin + PWM 
  
  //chain left
  const int in3 = 15;   //digitalPin + PWM 
  const int in4 = 14;   //digitalPin + PWM 
  
  //Serial
  const unsigned long baudRate = 1000000;
#endif
