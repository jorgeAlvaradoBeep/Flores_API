#include <ArduinoJson.h>
#include <HTTPClient.h>
#include <WiFi.h>

const char *ssid = "Tostatronic_Guest";
const char *password = "Tosta19951$";

//String user_agent = "Mozilla/5.0 Gecko/20100101";

void setup() {
  Serial.begin(115200);
  
  // Conectar a la red WiFi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Conectando a WiFi...");
  }
  Serial.println("Conectado a la WiFi");
}

void loop() {
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
    doc["IdDatoMedicion"] = 1;
    doc["Valor"] = "25";
    doc["Comentario"] = "Humedad por debajo de la optima.";
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
    } else {
      Serial.print("Error code: ");
      Serial.println(httpResponseCode);
      Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpResponseCode).c_str());
    }
    // Cerrar la conexión
    http.end();
  }
  delay(30000); // Espera 30 segundos antes de enviar los datos de nuevo
}
