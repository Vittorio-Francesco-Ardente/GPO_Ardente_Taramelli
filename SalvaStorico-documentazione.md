💾 Funzione SalvaStorico_Click
📋 Informazioni Generali
Campo	Valore
📁 File	SalvaStorico_Function.txt
👥 Autori	Ardente, Taramelli
🎓 Classe	5^Ci
📅 Anno Scolastico	2025/26
🔢 Versione	1.1
🎯 Descrizione Generale
Questa funzione salva i dettagli dell'ordine corrente su un file di testo denominato StoricoOrdini.txt. Il salvataggio avviene in modalità APPEND, quindi ogni nuovo ordine viene aggiunto in coda senza sovrascrivere i precedenti.

📝 Contenuto di ogni riga
Ogni riga del file storico contiene: - 🆔 ID progressivo dell'ordine - 📅 Data e ora del salvataggio - 🍕 Dettaglio delle pizze ordinate (raggruppate per tipo con quantità)

💡 A Cosa Serve
La funzione è fondamentale per:

📚 Mantenere uno storico persistente di tutti gli ordini effettuati
📊 Permettere analisi successive sulle vendite
🕒 Tracciare l'attività della pizzeria nel tempo
📈 Fornire dati per statistiche e reportistica
🔒 Backup delle informazioni ordini in caso di necessità
⚙️ Come Funziona
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
🔄 Flusso operativo
✅ Verifica che ci siano pizze nell'ordine
🔍 Verifica la validità del percorso file
🔒 Controlla i permessi di scrittura nella directory
📊 Raggruppa le pizze per tipo usando LINQ
📝 Formatta la riga di log con ID, data/ora e dettaglio pizze
💾 Scrive sul file in modalità append (aggiunge in coda)
✅ Conferma l'avvenuto salvataggio all'utente
🛡️ Controlli Eseguiti
#	Controllo	Descrizione
✅ 1	Inizializzazione	Verifica inizializzazione controllo Ordini_Lista
✅ 2	Ordine vuoto	Verifica presenza almeno una pizza nell'ordine
✅ 3	Percorso valido	Verifica validità percorso file
✅ 4	Permessi	Verifica permessi di scrittura nella directory
✅ 5	Elementi validi	Filtraggio e validazione elementi della lista
✅ 6	File bloccato	Verifica file non bloccato da altro processo
✅ 7	Eccezioni I/O	Gestione eccezioni I/O multiple (dettagliate)
📄 Formato File Output
Il file StoricoOrdini.txt ha questo formato:

ID: 0001 | DATA/ORA: 15/01/2026 14:30:25 | ORDINE: 2x Margherita, 1x Capricciosa
ID: 0002 | DATA/ORA: 15/01/2026 15:45:12 | ORDINE: 1x Marinara, 3x Quattro formaggi
ID: 0003 | DATA/ORA: 15/01/2026 16:20:08 | ORDINE: 1x Salmone, 1x Vegetariana
🔍 Struttura Riga
Campo	Formato	Descrizione
ID	XXXX	Numero progressivo ordine (4 cifre, con zeri iniziali)
DATA/ORA	dd/MM/yyyy HH:mm:ss	Timestamp del salvataggio
ORDINE	Nx NomePizza	Elenco pizze raggruppate (separati da virgola)
🔌 Collegamento al Form
Metodo 1: Nel costruttore
// Nel costruttore Form1()
this.SalvaStorico_Button.Click += new System.EventHandler(this.SalvaStorico_Click);
Metodo 2: Direttamente nel codice
SalvaStorico_Button.Click += SalvaStorico_Click;
🧪 Test di Sistema Correlati
Test	Descrizione
TEST 19	❌ Salvataggio Ordine Vuoto
TEST 20	✅ Salvataggio Ordine Valido
TEST 21	📝 Salvataggio Append File
TEST 22	📊 Salvataggio Formato Dati
TEST 23	⚠️ Salvataggio Errore Scrittura
TEST 24	🔒 Salvataggio Tavolo Occupato
🧰 Metodi per Test Automatizzati
1️⃣ SalvaStoricoPerTest()
public (bool success, string messaggio) SalvaStoricoPerTest(string percorsoFile = null)
📝 Salva lo storico senza visualizzare MessageBox
🎯 Utile per Unit Test
↩️ Ritorna: tupla (success, messaggio)
2️⃣ VerificaEsistenzaFileStoricoPerTest()
public bool VerificaEsistenzaFileStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
🔍 Verifica l'esistenza del file storico
↩️ Ritorna: true se il file esiste, false altrimenti
3️⃣ LeggiUltimaRigaStoricoPerTest()
public string LeggiUltimaRigaStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
📖 Legge l'ultima riga del file storico
↩️ Ritorna: ultima riga del file, o stringa vuota se errore
4️⃣ ContaRigheStoricoPerTest()
public int ContaRigheStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
🔢 Conta il numero di righe nel file storico
↩️ Ritorna: numero di righe, o -1 se errore
💻 Codice Sorgente
private void SalvaStorico_Click(object sender, EventArgs e)
{
    const string NOME_FILE_STORICO = "StoricoOrdini.txt";

    // CONTROLLO 1: Verifica inizializzazione
    if (Ordini_Lista == null)
    {
        MessageBox.Show(
            "⚠️ Errore di inizializzazione del controllo ordini.\n\n" +
            "🔄 Riavviare l'applicazione.",
            "❌ Errore Sistema",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
        return;
    }

    // CONTROLLO 2: Verifica ordine non vuoto
    if (Ordini_Lista.Items.Count == 0)
    {
        MessageBox.Show(
            "📋 Nessun ordine da salvare.\n\n" +
            "🍕 Aggiungi almeno una pizza all'ordine prima di salvare lo storico.",
            "ℹ️ Ordine Vuoto",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
        return;
    }

    // CONTROLLO 3: Verifica validità percorso
    string percorsoCompleto;
    try
    {
        percorsoCompleto = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            NOME_FILE_STORICO
        );

        if (string.IsNullOrWhiteSpace(percorsoCompleto))
        {
            throw new ArgumentException("Percorso file non valido.");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            $"⚠️ Errore nella configurazione del percorso file:\n\n" +
            $"❌ {ex.Message}",
            "❌ Errore Percorso",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
        return;
    }

    // CONTROLLO 4: Verifica permessi scrittura
    try
    {
        string directory = Path.GetDirectoryName(percorsoCompleto);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
    catch (UnauthorizedAccessException)
    {
        MessageBox.Show(
            "🔒 Non si dispone dei permessi necessari per scrivere nella directory.\n\n" +
            "📞 Contattare l'amministratore di sistema.",
            "❌ Permessi Negati",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
        return;
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            $"⚠️ Errore durante la verifica della directory:\n\n" +
            $"❌ {ex.Message}",
            "❌ Errore Directory",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
        return;
    }

    try
    {
        // CONTROLLO 5: Filtra elementi validi
        var elementiValidi = Ordini_Lista.Items
            .Cast<object>()
            .Where(item => item != null)
            .Select(item => item.ToString())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();

        if (elementiValidi.Count == 0)
        {
            MessageBox.Show(
                "⚠️ L'ordine non contiene elementi validi da salvare.",
                "❌ Ordine Non Valido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            return;
        }

        // Raggruppa pizze per tipo
        var dettaglioPizze = elementiValidi
            .GroupBy(p => p)
            .Select(g => $"{g.Count()}x {g.Key}");

        string stringaPizze = string.Join(", ", dettaglioPizze);

        // Costruisce riga log
        StringBuilder logLine = new StringBuilder();
        logLine.Append($"ID: {idOrdineProgressivo:0000} | ");
        logLine.Append($"DATA/ORA: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | ");
        logLine.Append($"ORDINE: {stringaPizze}");

        // CONTROLLO 6: Verifica file non bloccato
        if (File.Exists(percorsoCompleto))
        {
            try
            {
                using (FileStream fs = File.Open(percorsoCompleto, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // File accessibile
                }
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "🔒 Il file dello storico è attualmente in uso da un altro programma.\n\n" +
                    "📝 Chiudere l'applicazione che sta utilizzando il file e riprovare.",
                    "⚠️ File in Uso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
        }

        // Scrittura su file (APPEND)
        File.AppendAllText(
            percorsoCompleto,
            logLine.ToString() + Environment.NewLine,
            Encoding.UTF8
        );

        // Conferma operazione
        MessageBox.Show(
            $"✅ Ordine #{idOrdineProgressivo:0000} salvato correttamente nello storico!\n\n" +
            $"📝 File: {NOME_FILE_STORICO}\n" +
            $"📂 Percorso: {percorsoCompleto}\n\n" +
            $"👥 Ardente & Taramelli - 5^Ci - 2025/26",
            "💾 Salvataggio Completato",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }
    // CONTROLLO 7: Gestione eccezioni I/O
    catch (UnauthorizedAccessException ex)
    {
        MessageBox.Show(
            $"🔒 Accesso negato durante la scrittura del file:\n\n" +
            $"❌ {ex.Message}\n\n" +
            "🔑 Verificare i permessi di scrittura.",
            "❌ Errore Permessi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
    catch (DirectoryNotFoundException ex)
    {
        MessageBox.Show(
            $"📁 Directory non trovata:\n\n" +
            $"❌ {ex.Message}",
            "❌ Errore Directory",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
    catch (PathTooLongException ex)
    {
        MessageBox.Show(
            $"📏 Il percorso del file è troppo lungo:\n\n" +
            $"❌ {ex.Message}",
            "❌ Errore Percorso",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
    catch (IOException ex)
    {
        MessageBox.Show(
            $"💾 Errore di I/O durante il salvataggio:\n\n" +
            $"❌ {ex.Message}\n\n" +
            "💿 Verificare che il disco non sia pieno e che il file non sia protetto.",
            "❌ Errore I/O",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            $"⚠️ Si è verificato un errore imprevisto durante il salvataggio:\n\n" +
            $"📋 Tipo: {ex.GetType().Name}\n" +
            $"❌ Messaggio: {ex.Message}\n\n" +
            "📞 Contattare l'assistenza tecnica.",
            "❌ Errore Salvataggio",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
📊 Esempio di Output
ID: 0001 | DATA/ORA: 15/01/2026 14:30:25 | ORDINE: 2x Margherita, 1x Capricciosa
ID: 0002 | DATA/ORA: 15/01/2026 15:45:12 | ORDINE: 1x Marinara, 3x Quattro formaggi
ID: 0003 | DATA/ORA: 15/01/2026 16:20:08 | ORDINE: 1x Salmone, 1x Vegetariana, 2x Americana
ID: 0004 | DATA/ORA: 15/01/2026 17:05:33 | ORDINE: 4x Margherita
ID: 0005 | DATA/ORA: 15/01/2026 18:15:47 | ORDINE: 1x Prosciutto e funghi, 1x Salamino
👥 Credits
Sviluppato da: Ardente & Taramelli
Classe: 5^Ci
Anno Scolastico: 2025/26
Versione: 1.1

💡 Nota: Questa funzione fa parte del sistema gestionale per pizzerie sviluppato come progetto scolastico.
