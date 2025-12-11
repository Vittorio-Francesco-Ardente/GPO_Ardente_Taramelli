# 💾 Funzione `SalvaStorico_Click`

## 📋 Informazioni Generali

| Campo | Valore |
|-------|--------|
| **📁 File** | `SalvaStorico_Function.txt` |
| **👥 Autori** | Ardente, Taramelli |
| **🎓 Classe** | 5^Ci |
| **📅 Anno Scolastico** | 2025/26 |
| **🔢 Versione** | 1.1 |

---

## 🎯 Descrizione Generale

Questa funzione **salva i dettagli dell'ordine corrente** su un file di testo denominato `StoricoOrdini.txt`. Il salvataggio avviene in **modalità APPEND**, quindi ogni nuovo ordine viene aggiunto in coda senza sovrascrivere i precedenti.

### 📝 Contenuto di ogni riga

Ogni riga del file storico contiene:
- 🆔 **ID progressivo** dell'ordine
- 📅 **Data e ora** del salvataggio
- 🍕 **Dettaglio delle pizze** ordinate (raggruppate per tipo con quantità)

---

## 💡 A Cosa Serve

La funzione è fondamentale per:

1. 📚 **Mantenere uno storico persistente** di tutti gli ordini effettuati
2. 📊 **Permettere analisi successive** sulle vendite
3. 🕒 **Tracciare l'attività** della pizzeria nel tempo
4. 📈 **Fornire dati** per statistiche e reportistica
5. 🔒 **Backup** delle informazioni ordini in caso di necessità

---

## ⚙️ Come Funziona

```mermaid
graph TD
    A[👤 Utente clicca Salva Storico] --> B{🍕 Ci sono pizze?}
    B -->|❌ No| C[⚠️ Messaggio: Ordine vuoto]
    B -->|✅ Sì| D{📂 Percorso valido?}
    D -->|❌ No| E[⚠️ Errore percorso]
    D -->|✅ Sì| F{🔓 Permessi OK?}
    F -->|❌ No| G[⚠️ Permessi negati]
    F -->|✅ Sì| H[📊 Raggruppa pizze LINQ]
    H --> I[📝 Formatta riga log]
    I --> J[💾 Scrivi su file APPEND]
    J --> K[✅ Conferma salvataggio]
