# 🌱 msAlertaMongoDB  

Microservice responsible for managing **environmental alerts** 🌍.  
It is part of a larger **Environmental License System** 🏭📜, helping organizations track and monitor alerts related to licenses and environmental activities.  

---

## 🚀 Features  

- 📡 **Create Alerts** → Register new alerts in MongoDB.  
- 🔎 **Retrieve Alerts** → Search alerts by ID or list all available.  
- ✏️ **Update Alerts** → Modify alert information when necessary.  
- ❌ **Delete Alerts** → Remove alerts no longer needed.  

---

## 🛠️ Tech Stack  

- 💻 **.NET 8** — WebAPI framework.  
- 🍃 **MongoDB** — NoSQL database for storing alerts.  
- 🎨 **AutoMapper** — For easy DTO ↔ Entity mapping.  
- 📑 **Swagger** — API documentation and testing.  

---
```text
## 📂 Project Structure  

msAlertaMongoDB/
- **Controllers/**       # API endpoints (AlertaController.cs)
- **DTO/**               # Data Transfer Objects (Request & Response DTOs)
- **Entity/**            # Domain entities (Alerta.cs)
- **Repository/**        # Database persistence logic
- **Service/**           # Business logic layer
- **Program.cs**         # Entry point
- **appsettings.json**   # Configuration (MongoDB, logging, etc.)


## 🔄 DTOs  

- **AlertaRequestDto** 📥 → Used for creating/updating alerts (does **not** contain ID).  
- **AlertaResponseDto** 📤 → Used for returning alerts to the client (contains **ID**).  

---

## 📖 Example API Usage  

### ▶️ Create Alert  
```http
POST /api/alerta
{
  "titulo": "High CO2 emission detected",
  "descricao": "CO2 levels exceeded safe threshold",
  "nivel": "ALTO"
}
