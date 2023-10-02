#include <OneWire.h>
#include <DallasTemperature.h>
#include "DHT.h"

//Definiciones globales
#define TIEMPO_ESPERA_LECTURAS 5000

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

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);

  //Iniciación para el sensor de tenperatura de suelo
  sensors.begin();

  //Iniciamos el dht22
  dht.begin();


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
  Serial.print("Temperatura: ");
  Serial.print(sensors.getTempCByIndex(0)); 
  Serial.println(" *C");
}

void MidrHumedadSuelo()
{
  int valorAnalogico = analogRead(HUMEDAD_PIN);
  float porcentajeHumedad = map(valorAnalogico, VALOR_MOJADO, VALOR_SECO, 100, 0);
  porcentajeHumedad = constrain(porcentajeHumedad, 0, 100);

  Serial.print("Humedad: ");
  Serial.print(porcentajeHumedad);
  Serial.println(" %");
}

void MideHumedadYTemperaturaAmbiental()
{
  float h = dht.readHumidity();
  float t = dht.readTemperature();

  Serial.print("Humedad: ");
  Serial.print(h);
  Serial.print(" %\t");
  Serial.print("Temperatura: ");
  Serial.print(t);
  Serial.println(" *C");
}