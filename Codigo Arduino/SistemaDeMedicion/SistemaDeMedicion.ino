#include <OneWire.h>
#include <DallasTemperature.h>
#include "DHT.h"
#include <ArduinoJson.h>
#include <HTTPClient.h>
#include <WiFi.h>

//Definiciones globales
#define TIEMPO_ESPERA_LECTURAS 30000

//Definiciones para el sensor de temperatura de suelo
#define ONE_WIRE_BUS 5

//Definicion para el sensor de humedad de suelo
#define HUMEDAD_PIN 34
#define VALOR_SECO 4095 // valor cuando el sensor está completamente seco
#define VALOR_MOJADO 0  // valor cuando el sensor está completamente mojado

//Definición para el dht22
#define DHTPIN 4
#define DHTTYPE DHT22

//Declaraciones de variables para el sensor de temperatura de suelo
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);

//Declaracion del DHT22
DHT dht(DHTPIN, DHTTYPE);

//Declaración de las variables para la conexión
const char *ssid = "Tostatronic_Guest";
const char *password = "Tosta19951$";

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);

  //Iniciación para el sensor de tenperatura de suelo
  sensors.begin();

  //Iniciamos el dht22
  dht.begin();

  //iniciamos la conexión al WiFi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Conectando a WiFi...");
  }
  Serial.println("Conectado a la WiFi");
}

void loop() {
  // put your main code here, to run repeatedly:
  MideTemperaturaSuelo();
  MidrHumedadSuelo();
  MideHumedadYTemperaturaAmbiental();

  delay(TIEMPO_ESPERA_LECTURAS);
}

void MideTemperaturaSuelo()
{
  sensors.requestTemperatures();
  float t = sensors.getTempCByIndex(0);
  Serial.print("Temperatura: ");
  Serial.print(t); 
  Serial.println(" *C");
  String comentario;
  if(t>20)
    comentario = "Temperatura De Tierra Alta.";
  else if(t>10 && t<=20)
    comentario = "Temperatura De Tierra Normal.";
  else 
    comentario = "Temperatura De Tierra Baja.";
  
  EnviaDatosDeMedicion(3, String(t), comentario);
}

void MidrHumedadSuelo()
{
  int valorAnalogico = analogRead(HUMEDAD_PIN);
  float porcentajeHumedad = map(valorAnalogico, VALOR_MOJADO, VALOR_SECO, 100, 0);
  porcentajeHumedad = constrain(porcentajeHumedad, 0, 100);

  Serial.print("Humedad Suelo: ");
  Serial.print(porcentajeHumedad);
  Serial.println(" %");
  String comentario;
  if(porcentajeHumedad>55)
    comentario = "Humedad De Tierra Normal.";
  else
    comentario = "Humedad De Tierra Baja.";
  
  
  EnviaDatosDeMedicion(1, String(porcentajeHumedad), comentario);
}

void MideHumedadYTemperaturaAmbiental()
{
  float h = dht.readHumidity();
  float t = dht.readTemperature();

  Serial.print("Humedad Ambiental: ");
  Serial.print(h);
  Serial.print(" %\t");
  Serial.print("Temperatura Ambiental: ");
  Serial.print(t);
  Serial.println(" *C");
  String comentario;
  if(h>30)
    comentario = "Humedad Ambiental Elevada.";
  else
    comentario = "Humedad Ambiental Normal.";
  
  
  EnviaDatosDeMedicion(2, String(h), comentario);

  if(t>30)
    comentario = "Temperatura Ambiental Elevada.";
  else if(t>20 && t<=30)
    comentario = "Temperatura Ambiental Normal.";
  else 
    comentario = "Temperatura Ambiental Baja.";
  
  EnviaDatosDeMedicion(4, String(t), comentario);
}

bool EnviaDatosDeMedicion(int idDatoMedicion, String valor, String comentario)
{
  bool succes;
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;

    // URL del servidor al que enviarás los datos
    
    //http.begin("http://192.168.68.113:5249/api/Mediciones/Create/");
    http.begin("http://192.168.100.29:5249/api/Mediciones/Create/");
    //http.setUserAgent(user_agent);
    
    http.addHeader("Content-Type", "application/json");
    
    // Crear el objeto JSON
    DynamicJsonDocument doc(1024);
    doc["IdMedicion"] = 0;
    doc["IdFlor"] = 1;
    doc["IdDatoMedicion"] = idDatoMedicion;
    doc["Valor"] = valor;
    doc["Comentario"] = comentario;
    doc["Fecha"] = "2023-10-2 10:19:55";
    
    // Convertir el objeto JSON a String
    String requestBody;
    serializeJson(doc, requestBody);
    
    // Enviar el POST request
    int httpResponseCode = http.POST(requestBody);

    if (httpResponseCode > 0) {
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);
      String payload = http.getString();
      Serial.println(payload);
      succes = true;
    } else {
      Serial.print("Error code: ");
      Serial.println(httpResponseCode);
      Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpResponseCode).c_str());
      succes = false;
    }
    // Cerrar la conexión
    http.end();
  }
  else
    succes = false;
  return succes;
}