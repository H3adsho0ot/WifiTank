#include <ArduinoJson.h>

//chassis
//chain right
const int ENA = 9;
const int in1 = 8;
const int in2 = 10;

//chain left
const int ENB = 11;
const int in3 = 13;
const int in4 = 12;

//https://arduinojson.org/v6/assistant/
StaticJsonDocument<194> jsonObject;

int LCS = 0;
int RCS = 0;
bool LCF = true;
bool RCF = true;
  
void setup()
{
  setupChassis();
  Serial.begin(1000000);
}

void loop()
{
  //Check if serial available
  if (Serial.available() > 0)
  {
    String inputString = Serial.readStringUntil('\n');
    int inputStringLen = inputString.length() + 1; 
    char input[inputStringLen];  //= "{\"leftChainSpeed\": 80, \"leftChainForward\":true, \"rightChainSpeed\": 80, \"rightChainForward\":false}";
    
    inputString.toCharArray(input, inputStringLen);

    // Deserialize JSON
    DeserializationError error = deserializeJson(jsonObject, input);
    
    // Test if parsing succeeds.
    if (!error) 
    {
      LCS = (int)jsonObject["LCS"];
      LCF = (bool)jsonObject["LCF"];
      RCS = (int)jsonObject["RCS"];
      RCF = (bool)jsonObject["RCF"];
    }
    else
    {
      //Serial.println("JSON deserializion error");
    }
  }

  driveChassis(LCS, LCF, RCS, RCF);
}

void setupChassis()
{
  pinMode(ENA, OUTPUT);    
  pinMode(ENB, OUTPUT);
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  pinMode(in3, OUTPUT);
  pinMode(in4, OUTPUT);
}

void driveChassis(int leftChainSpeed, bool leftChainForward, int rightChainSpeed, bool rightChainForward)
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
