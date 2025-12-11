# 🧾 Funzione `MostraScontrino_Click`

## 📋 Informazioni Generali

| Campo | Valore |
|-------|--------|
| **📁 File** | `MostraScontrino_Function.txt` |
| **👥 Autori** | Ardente, Taramelli |
| **🎓 Classe** | 5^Ci |
| **📅 Anno Scolastico** | 2025/26 |
| **🔢 Versione** | 1.1 |

---

## 🎯 Descrizione Generale

Questa funzione **calcola il totale dell'ordine corrente** e genera uno **scontrino fiscale formattato** che viene visualizzato a video tramite una MessageBox.

### 📝 Contenuto dello scontrino

Lo scontrino include:
- 🏪 **Intestazione** con nome pizzeria (Ardente-Taramelli)
- 📅 **Data e ora** corrente
- 🔢 **Numero progressivo** dell'ordine
- 🍕 **Dettaglio delle pizze** raggruppate per tipo con quantità
- 💶 **Prezzo unitario** e subtotale per ogni tipo di pizza
- 💰 **Totale complessivo** dell'ordine
- 😊 **Messaggio di ringraziamento**

---

## 💡 A Cosa Serve

La funzione è fondamentale per:

1. 👁️ **Permettere al cliente** di visualizzare il conto prima del pagamento
2. 📊 **Fornire un riepilogo dettagliato** dell'ordine
3. 🧮 **Calcolare automaticamente** il totale basandosi sul listino prezzi
4. 📦 **Raggruppare pizze uguali** per una lettura più chiara
5. 🆔 **Generare un documento fiscale** con ID univoco per tracciabilità

---

## ⚙️ Come Funziona

```mermaid
graph TD
    A[👤 Utente clicca Conto] --> B{🍕 Ci sono pizze?}
    B -->|❌ No| C[⚠️ Messaggio: Ordine vuoto]
    B -->|✅ Sì| D{💶 Listino disponibile?}
    D -->|❌ No| E[⚠️ Errore configurazione]
    D -->|✅ Sì| F[📊 Raggruppa pizze LINQ]
    F --> G[💰 Calcola subtotali]
    G --> H[➕ Somma totale]
    H --> I[🎨 Formatta scontrino ASCII]
    I --> J[📱 Mostra MessageBox]
