#include "Config.h"
#include "Chassis.cpp"
#include <ArduinoJson.h>
#include <WiFi.h>
#include <WebSocketsServer.h>

//https://arduinojson.org/v6/assistant/
StaticJsonDocument<80> jsonObject;

const char* ssid = "FRITZ!Box 7590 AuN";
const char* password = "pwd";
WebSocketsServer webSocket = WebSocketsServer(80);

int LCS = 0;
int RCS = 0;
bool LCF = true;
bool RCF = true;
Chassis chassis = Chassis();
  
void setup()
{
  chassis.setup();
  Serial.begin(baudRate);
  
  WiFi.begin(ssid, password);
  while ( WiFi.status() != WL_CONNECTED ) 
  {
    delay(500);
    Serial.print(".");
  }

  Serial.println("Connected!");
  Serial.print("My IP address: ");
  Serial.println(WiFi.localIP());
  
  webSocket.begin();
  webSocket.onEvent(onWebSocketEvent);
}

void onWebSocketEvent(uint8_t num, WStype_t type, uint8_t * payload, size_t length) 
{ 
  // Figure out the type of WebSocket event
  switch(type)
  { 
    // Client has disconnected
    case WStype_DISCONNECTED:
      Serial.printf("[%u] Disconnected!\n", num);
      break;
 
    // New client has connected
    case WStype_CONNECTED:
      {
        IPAddress ip = webSocket.remoteIP(num);
        Serial.printf("[%u] Connection from ", num);
        Serial.println(ip.toString());
      }
      break;
 
    // Echo text message back to client
    case WStype_TEXT:
       deserializeJSON((const char*)payload);
      //Serial.printf("[%u] Text: %s\n", num, payload);
      //webSocket.sendTXT(num, payload);
      break;
 
    // For everything else: do nothing
    case WStype_BIN:
    case WStype_ERROR:
    case WStype_FRAGMENT_TEXT_START:
    case WStype_FRAGMENT_BIN_START:
    case WStype_FRAGMENT:
    case WStype_FRAGMENT_FIN:
    default:
      break;
  }
}

void deserializeJSON(String inputString)
{
    int inputStringLen = inputString.length() + 1; 
    char input[inputStringLen];  //{"LCS":0,"LCF":true,"RCS":0,"RCF":true}
    
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

void loop()
{
  //Check if serial available
  if (Serial.available() > 0)
  {
    deserializeJSON(Serial.readStringUntil('\n'));
  }

  // Look for and handle WebSocket data
  webSocket.loop();

  chassis.drive(LCS, LCF, RCS, RCF);
}
