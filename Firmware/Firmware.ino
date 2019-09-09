#include "Config.h"
#include "Chassis.cpp"
#include <ArduinoJson.h>

//https://arduinojson.org/v6/assistant/
StaticJsonDocument<80> jsonObject;

int LCS = 0;
int RCS = 0;
bool LCF = true;
bool RCF = true;
Chassis chassis = Chassis();
  
void setup()
{
  chassis.setup();
  Serial.begin(baudRate);
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

  chassis.driveDRV8833(LCS, LCF, RCS, RCF);
}
