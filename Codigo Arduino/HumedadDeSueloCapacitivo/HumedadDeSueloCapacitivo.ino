#define HUMEDAD_PIN 34
#define VALOR_SECO 4095 // valor cuando el sensor está completamente seco
#define VALOR_MOJADO 0  // valor cuando el sensor está completamente mojado

void setup() {
  Serial.begin(115200);
}

void loop() {
  int valorAnalogico = analogRead(HUMEDAD_PIN);
  float porcentajeHumedad = map(valorAnalogico, VALOR_MOJADO, VALOR_SECO, 100, 0);
  porcentajeHumedad = constrain(porcentajeHumedad, 0, 100);

  Serial.print("Humedad: ");
  Serial.print(porcentajeHumedad);
  Serial.println(" %");

  delay(2000);
}
