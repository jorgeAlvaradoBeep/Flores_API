#include <OneWire.h>
#include <DallasTemperature.h>

#define ONE_WIRE_BUS 5

OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);

void setup(void) {
  Serial.begin(115200);
  sensors.begin();
}

void loop(void) { 
  sensors.requestTemperatures();
  Serial.print("Temperatura: ");
  Serial.print(sensors.getTempCByIndex(0)); 
  Serial.println(" *C");

  delay(2000);
}
