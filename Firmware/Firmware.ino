#include "esp_camera.h"
#include <ArduinoJson.h>
#include <WiFi.h>
#include <WebSocketsServer.h>

#define CAMERA_MODEL_AI_THINKER

#include "camera_pins.h"
#include "Config.h"
#include "Wifi_Secret.h"
#include "Chassis.cpp"


//https://arduinojson.org/v6/assistant/
StaticJsonDocument<80> jsonObject;

WebSocketsServer webSocket = WebSocketsServer(8080);
Chassis chassis = Chassis();

int LCS = 0;
int RCS = 0;
bool LCF = true;
bool RCF = true;

void setup()
{
  chassis.setup();
  Serial.begin(baudRate);
  Serial.setDebugOutput(true);

  camera_config_t config;
  config.ledc_channel = LEDC_CHANNEL_0;
  config.ledc_timer = LEDC_TIMER_0;
  config.pin_d0 = Y2_GPIO_NUM;
  config.pin_d1 = Y3_GPIO_NUM;
  config.pin_d2 = Y4_GPIO_NUM;
  config.pin_d3 = Y5_GPIO_NUM;
  config.pin_d4 = Y6_GPIO_NUM;
  config.pin_d5 = Y7_GPIO_NUM;
  config.pin_d6 = Y8_GPIO_NUM;
  config.pin_d7 = Y9_GPIO_NUM;
  config.pin_xclk = XCLK_GPIO_NUM;
  config.pin_pclk = PCLK_GPIO_NUM;
  config.pin_vsync = VSYNC_GPIO_NUM;
  config.pin_href = HREF_GPIO_NUM;
  config.pin_sscb_sda = SIOD_GPIO_NUM;
  config.pin_sscb_scl = SIOC_GPIO_NUM;
  config.pin_pwdn = PWDN_GPIO_NUM;
  config.pin_reset = RESET_GPIO_NUM;
  config.xclk_freq_hz = 20000000;
  config.pixel_format = PIXFORMAT_JPEG;

  //config.frame_size = FRAMESIZE_UXGA;
  //config.jpeg_quality = 10;
  //config.fb_count = 2;
  config.frame_size = FRAMESIZE_SVGA;
  config.jpeg_quality = 12;
  config.fb_count = 1;

  // camera init
  esp_err_t err = esp_camera_init(&config);
  if (err != ESP_OK)
  {
    Serial.printf("Camera init failed with error 0x%x", err);
    return;
  }

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
  switch (type)
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

  camera_fb_t *fb = esp_camera_fb_get();
  webSocket.broadcastBIN((const uint8_t*) fb->buf, fb->len);
  esp_camera_fb_return(fb);
}
