/// ============================================================================
/// @file Form1.cs
/// @brief Form principale per la gestione degli ordini di pizze
/// 
/// @author Ardente, Taramelli
/// @class 5^Ci
/// @version 1.1
/// @date Anno Scolastico 2025/26
/// 
/// @details Questo file contiene la logica principale dell'applicazione
///          per la gestione degli ordini di pizze in una pizzeria.
///          
///          Funzionalità principali:
///          - Aggiunta e rimozione pizze dall'ordine
///          - Visualizzazione pizze su tavolo virtuale
///          - Gestione ciclo completo servizio/sparecchiatura
///          - Generazione scontrino fiscale con calcolo totale
///          - Salvataggio storico ordini su file
///          
/// @copyright (c) 2025/26 - Ardente & Taramelli - 5^Ci
///            Progetto didattico - Tutti i diritti riservati
/// ============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;              // Gestione file (Storico ordini)
using System.Linq;            // Query LINQ su liste (Raggruppamento ordini)
using System.Text;            // StringBuilder (Scontrino/Log)

namespace Applicativo_Ardente
{
    /// <summary>
    /// Form principale dell'applicazione per la gestione degli ordini di pizze.
    /// </summary>
    /// <remarks>
    /// <para><b>Autori:</b> Ardente, Taramelli</para>
    /// <para><b>Classe:</b> 5^Ci</para>
    /// <para><b>Anno Scolastico:</b> 2025/26</para>
    /// <para><b>Versione:</b> 1.1</para>
    /// 
    /// <para><b>Descrizione:</b></para>
    /// <para>
    /// Questa classe implementa l'interfaccia principale per la gestione
    /// di una pizzeria. Permette di:
    /// </para>
    /// <list type="bullet">
    ///   <item>Selezionare pizze da un menu predefinito</item>
    ///   <item>Aggiungere pizze a un ordine (max 6 per ordine)</item>
    ///   <item>Rimuovere pizze dall'ordine</item>
    ///   <item>Servire l'ordine visualizzando le pizze sul tavolo</item>
    ///   <item>Sparecchiare il tavolo per nuovo ordine</item>
    ///   <item>Generare scontrino fiscale con dettaglio prezzi</item>
    ///   <item>Salvare storico ordini su file di testo</item>
    /// </list>
    /// </remarks>
    /// 
    /// ========================================================================
    /// TEST DI SISTEMA DA ESEGUIRE
    /// ========================================================================
    /// 
    /// --- TEST FUNZIONALITÀ BASE (1-12) ---
    /// 
    /// TEST 1 - Aggiunta Pizze: 
    ///          Selezionare pizze e verificare che vengano aggiunte correttamente alla lista ordini
    /// 
    /// TEST 2 - Limite Ordini: 
    ///          Tentare di aggiungere più di 6 pizze e verificare messaggio di errore
    /// 
    /// TEST 3 - Rimozione Pizza: 
    ///          Selezionare una pizza dall'ordine e rimuoverla, verificare che la lista si aggiorni
    /// 
    /// TEST 4 - Servizio Ordine Vuoto: 
    ///          Tentare di servire senza pizze nell'ordine, verificare messaggio di errore
    /// 
    /// TEST 5 - Servizio Ordine: 
    ///          Aggiungere pizze e servire, verificare visualizzazione corretta sul tavolo
    /// 
    /// TEST 6 - Modifica Durante Servizio: 
    ///          Con tavolo occupato, tentare di aggiungere/rimuovere pizze, verificare blocco
    /// 
    /// TEST 7 - Sparecchiatura: 
    ///          Sparecchiare il tavolo e verificare che torni al pannello ordini pulito
    /// 
    /// TEST 8 - Conferma Sparecchiatura: 
    ///          Verificare che appaia la richiesta di conferma prima di sparecchiare
    /// 
    /// TEST 9 - Gestione Focus: 
    ///          Verificare che il focus si sposti correttamente tra i controlli
    /// 
    /// TEST 10 - Stato Pulsanti: 
    ///           Verificare che i pulsanti si abilitino/disabilitino correttamente in ogni stato
    /// 
    /// TEST 11 - Immagini Mancanti: 
    ///           Rimuovere un'immagine e verificare che appaia il placeholder
    /// 
    /// TEST 12 - Posizionamento: 
    ///           Testare con 1, 3, 4, 6 pizze e verificare il layout corretto
    /// 
    /// --- TEST NUOVE FUNZIONALITÀ - SCONTRINO (13-18) ---
    /// 
    /// TEST 13 - Scontrino Ordine Vuoto: 
    ///           Tentare di visualizzare lo scontrino senza pizze nell'ordine.
    ///           RISULTATO ATTESO: Messaggio di avviso "Nessun ordine presente".
    /// 
    /// TEST 14 - Scontrino Con Ordine Valido:
    ///           Aggiungere pizze all'ordine e richiedere lo scontrino.
    ///           RISULTATO ATTESO: Visualizzazione corretta con dettaglio prezzi,
    ///                            quantità raggruppate e totale complessivo.
    /// 
    /// TEST 15 - Scontrino Prezzi Non Configurati:
    ///           Tentare di calcolare lo scontrino con una pizza non presente
    ///           nel listino prezzi.
    ///           RISULTATO ATTESO: Utilizzo del prezzo predefinito (€ 6,00).
    /// 
    /// TEST 16 - Scontrino Raggruppamento Pizze:
    ///           Aggiungere più pizze dello stesso tipo e verificare il raggruppamento.
    ///           RISULTATO ATTESO: Le pizze uguali sono raggruppate (es. "3x Margherita").
    /// 
    /// TEST 17 - Scontrino Calcolo Totale:
    ///           Verificare che il totale sia calcolato correttamente.
    ///           RISULTATO ATTESO: Somma corretta di tutti i subtotali delle righe.
    /// 
    /// TEST 18 - Scontrino ID Ordine:
    ///           Verificare che l'ID ordine progressivo sia visualizzato correttamente.
    ///           RISULTATO ATTESO: ID univoco incrementale per ogni ordine.
    /// 
    /// --- TEST NUOVE FUNZIONALITÀ - SALVATAGGIO STORICO (19-25) ---
    /// 
    /// TEST 19 - Salvataggio Ordine Vuoto:
    ///           Tentare di salvare lo storico senza pizze nell'ordine.
    ///           RISULTATO ATTESO: Messaggio informativo "Nessun ordine da salvare".
    /// 
    /// TEST 20 - Salvataggio Ordine Valido:
    ///           Aggiungere pizze e salvare lo storico.
    ///           RISULTATO ATTESO: File "StoricoOrdini.txt" creato/aggiornato
    ///                            con i dati dell'ordine.
    /// 
    /// TEST 21 - Salvataggio Append File:
    ///           Salvare più ordini consecutivi.
    ///           RISULTATO ATTESO: Ogni ordine viene aggiunto in coda al file
    ///                            senza sovrascrivere i precedenti.
    /// 
    /// TEST 22 - Salvataggio Formato Dati:
    ///           Verificare il formato dei dati salvati.
    ///           RISULTATO ATTESO: Formato "ID: X | DATA/ORA: dd/MM/yyyy HH:mm:ss | ORDINE: dettagli"
    /// 
    /// TEST 23 - Salvataggio Errore Scrittura:
    ///           Simulare un errore di scrittura (file in uso, permessi negati).
    ///           RISULTATO ATTESO: Messaggio di errore con dettagli dell'eccezione.
    /// 
    /// TEST 24 - Salvataggio Tavolo Occupato:
    ///           Tentare di salvare lo storico mentre il tavolo è occupato.
    ///           RISULTATO ATTESO: Operazione consentita (lo storico può essere
    ///                            salvato anche durante il servizio).
    /// 
    /// TEST 25 - Scontrino Tavolo Occupato:
    ///           Richiedere lo scontrino mentre il tavolo è occupato.
    ///           RISULTATO ATTESO: Scontrino visualizzato correttamente
    ///                            (il conto può essere richiesto durante il servizio).
    /// 
    /// ========================================================================
    /// 
    public partial class Form1 : Form
    {
        #region Costanti

        /// <summary>
        /// Numero massimo di pizze ordinabili per singolo ordine
        /// </summary>
        private const int MAX_PIZZE_ORDINE = 6;

        /// <summary>
        /// Dimensione in pixel di ogni immagine pizza visualizzata sul tavolo
        /// </summary>
        private const int DIMENSIONE_PIZZA = 165;

        /// <summary>
        /// Spaziatura orizzontale in pixel tra le pizze visualizzate sul tavolo
        /// </summary>
        private const int SPAZIATURA_PIZZE = 140;

        /// <summary>
        /// Coordinata X di partenza per il posizionamento delle pizze
        /// </summary>
        private const int POSIZIONE_INIZIALE_X = 140;

        /// <summary>
        /// Coordinata Y di partenza per il posizionamento delle pizze
        /// </summary>
        private const int POSIZIONE_INIZIALE_Y = 40;

        /// <summary>
        /// Spaziatura verticale in pixel tra le righe di pizze
        /// </summary>
        private const int SPAZIATURA_VERTICALE = 225;

        #endregion

        #region Campi Privati

        /// <summary>
        /// Dizionario che mappa i nomi delle pizze ai percorsi delle relative immagini.
        /// Utilizzato per il caricamento dinamico delle immagini sul tavolo.
        /// </summary>
        private readonly Dictionary<string, string> pizzaImages = new Dictionary<string, string>
        {
            { "Margherita", @"Margherita.jpg" },
            { "Americana", @"Americana.jpg" },
            { "Caprese", @"Caprese.jpg" },
            { "Capricciosa", @"Capricciosa.jpg" },
            { "Marinara", @"Marinara.jpg" },
            { "Napoletana", @"Napoletana.jpg" },
            { "Prosciutto e funghi", @"Prosciutto e funghi.jpg" },
            { "Quattro formaggi", @"Quattro formaggi.jpg" },
            { "Salamino", @"Salamino.jpg" },
            { "Salmone", @"Salmone.jpg" },
            { "Tonno e cipolle", @"Tonno e cipolle.jpg" },
            { "Vegetariana", @"Vegetariana.jpg" },
            { "Wurstel e cipolle", @"Wurstel e cipolle.jpg" }
        };

        /// <summary>
        /// Listino prezzi per il calcolo dello scontrino.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// </remarks>
        private readonly Dictionary<string, decimal> listinoPrezzi = new Dictionary<string, decimal>
        {
            { "Margherita", 5.00m },
            { "Marinara", 4.50m },
            { "Americana", 7.50m },
            { "Caprese", 7.00m },
            { "Capricciosa", 8.00m },
            { "Napoletana", 6.00m },
            { "Prosciutto e funghi", 7.50m },
            { "Quattro formaggi", 8.50m },
            { "Salamino", 7.00m },
            { "Salmone", 9.00m },
            { "Tonno e cipolle", 7.50m },
            { "Vegetariana", 7.50m },
            { "Wurstel e cipolle", 7.00m }
        };

        /// <summary>
        /// Lista delle PictureBox create per visualizzare le pizze sul tavolo.
        /// Utilizzata per tenere traccia dei controlli da rimuovere durante la pulizia.
        /// </summary>
        private readonly List<PictureBox> pizzaPictureBoxes = new List<PictureBox>();

        /// <summary>
        /// Flag che indica se un ordine è attualmente servito sul tavolo.
        /// Previene operazioni non valide quando il tavolo è occupato.
        /// </summary>
        private bool tavoloOccupato = false;

        /// <summary>
        /// Flag che indica se è in corso un'operazione di servizio delle pizze.
        /// Previene operazioni concorrenti che potrebbero causare inconsistenze.
        /// </summary>
        private bool servizioInCorso = false;

        /// <summary>
        /// ID progressivo dell'ordine corrente, utilizzato per scontrini e storico.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// </remarks>
        private int idOrdineProgressivo = 1;

        #endregion

        #region Proprietà Pubbliche per Test

        /// <summary>
        /// Ottiene lo stato di occupazione del tavolo.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il tavolo è occupato, False altrimenti</returns>
        public bool IsTavoloOccupato => tavoloOccupato;

        /// <summary>
        /// Ottiene lo stato dell'operazione di servizio.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se un servizio è in corso, False altrimenti</returns>
        public bool IsServizioInCorso => servizioInCorso;

        /// <summary>
        /// Ottiene il numero di pizze attualmente visualizzate sul tavolo.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>Numero di PictureBox delle pizze sul tavolo</returns>
        public int NumeroPizzeSulTavolo => pizzaPictureBoxes.Count;

        /// <summary>
        /// Ottiene il numero di pizze nell'ordine corrente.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>Numero di pizze nella lista ordini</returns>
        public int NumeroPizzeInOrdine => Ordini_Lista?.Items.Count ?? 0;

        /// <summary>
        /// Ottiene il limite massimo di pizze per ordine.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>Costante MAX_PIZZE_ORDINE</returns>
        public int MaxPizzeOrdine => MAX_PIZZE_ORDINE;

        /// <summary>
        /// Ottiene lo stato di abilitazione del pulsante Servi.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il pulsante è abilitato, False altrimenti</returns>
        public bool IsServiButtonEnabled => Servi_Button?.Enabled ?? false;

        /// <summary>
        /// Ottiene lo stato di abilitazione del pulsante Cancella.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il pulsante è abilitato, False altrimenti</returns>
        public bool IsCancellaButtonEnabled => Cancella_Button?.Enabled ?? false;

        /// <summary>
        /// Ottiene lo stato di abilitazione del pulsante Aggiungi.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il pulsante è abilitato, False altrimenti</returns>
        public bool IsAggiungiButtonEnabled => Aggiungi_Button?.Enabled ?? false;

        /// <summary>
        /// Ottiene la visibilità del pannello ordini.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il pannello è visibile, False altrimenti</returns>
        public bool IsPanelOrdiniVisible => Panel_Ordini?.Visible ?? false;

        /// <summary>
        /// Ottiene la visibilità del pannello sparecchia.
        /// Proprietà esposta per permettere i test di sistema.
        /// </summary>
        /// <returns>True se il pannello è visibile, False altrimenti</returns>
        public bool IsPanelSparecchiaVisible => Panel_Sparecchia?.Visible ?? false;

        #endregion

        #region Costruttore

        /// <summary>
        /// Inizializza una nuova istanza del form principale.
        /// Configura lo stato iniziale dell'interfaccia e dei controlli.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// </remarks>
        public Form1()
        {
            InitializeComponent();

            // Imposta la visibilità iniziale dei pannelli
            Panel_Sparecchia.Visible = false;
            Panel_Ordini.Visible = true;

            // Configura lo stato iniziale dei pulsanti
            ConfiguraStatoInizialeControlli();

            // Registra gli event handler per la gestione dinamica dei controlli
            RegistraEventHandlers();
        }

        #endregion

        #region Inizializzazione

        /// <summary>
        /// Configura lo stato iniziale di tutti i controlli dell'interfaccia.
        /// Disabilita i pulsanti che non possono essere usati all'avvio.
        /// </summary>
        private void ConfiguraStatoInizialeControlli()
        {
            // Disabilita il pulsante di servizio (nessun ordine presente)
            if (Servi_Button != null)
            {
                Servi_Button.Enabled = false;
            }

            // Disabilita il pulsante di cancellazione (nessuna selezione)
            if (Cancella_Button != null)
            {
                Cancella_Button.Enabled = false;
            }

            // Il pulsante Aggiungi è inizialmente disabilitato (nessuna selezione)
            if (Aggiungi_Button != null)
            {
                Aggiungi_Button.Enabled = false;
            }
        }

        /// <summary>
        /// Registra gli event handler necessari per la gestione dinamica dei controlli.
        /// Permette l'aggiornamento automatico dello stato dell'interfaccia.
        /// </summary>
        private void RegistraEventHandlers()
        {
            // Event handler per la selezione nella lista pizze disponibili
            if (Lista_Pizze != null)
            {
                Lista_Pizze.SelectedIndexChanged += Lista_Pizze_SelectedIndexChanged;
            }

            // Event handler per la selezione nella lista ordini
            if (Ordini_Lista != null)
            {
                Ordini_Lista.SelectedIndexChanged += Ordini_Lista_SelectedIndexChanged;
            }
        }

        #endregion

        #region Event Handlers - Gestione Selezioni

        /// <summary>
        /// Gestisce il cambio di selezione nella lista delle pizze disponibili.
        /// Aggiorna lo stato del pulsante Aggiungi in base alla selezione e al numero di ordini.
        /// </summary>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Lista_Pizze_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aggiorna lo stato del pulsante Aggiungi
            if (Aggiungi_Button != null && Lista_Pizze != null && Ordini_Lista != null)
            {
                // Abilita solo se c'è una selezione, non si è al limite e il tavolo è libero
                Aggiungi_Button.Enabled = (Lista_Pizze.SelectedItem != null &&
                                           Ordini_Lista.Items.Count < MAX_PIZZE_ORDINE &&
                                           !tavoloOccupato);
            }
        }

        /// <summary>
        /// Gestisce il cambio di selezione nella lista degli ordini.
        /// Abilita o disabilita i pulsanti in base allo stato della selezione e della lista.
        /// </summary>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Ordini_Lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aggiorna lo stato dei pulsanti in base alla selezione
            AggiornaStatoPulsanti();
        }

        #endregion

        #region Event Handlers - Gestione Ordini

        /// <summary>
        /// Gestisce l'aggiunta di una pizza alla lista ordini.
        /// Esegue controlli approfonditi per prevenire errori e stati inconsistenti.
        /// </summary>
        /// <remarks>
        /// <para><b>Controlli Eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Verifica che il tavolo non sia occupato</item>
        ///   <item>Verifica inizializzazione controlli</item>
        ///   <item>Verifica selezione pizza</item>
        ///   <item>Verifica limite massimo ordini (6 pizze)</item>
        ///   <item>Verifica validità pizza selezionata</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Aggiungi_Button_Click(object sender, EventArgs e)
        {
            // Controllo 1: Verifica che il tavolo non sia occupato
            if (tavoloOccupato)
            {
                MessageBox.Show(
                    "🍽️ Non puoi modificare l'ordine mentre il tavolo è occupato!\n\n" +
                    "🧹 Sparecchia il tavolo prima di creare un nuovo ordine.",
                    "⚠️ Tavolo Occupato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Controllo 2: Verifica che i controlli siano inizializzati
            if (Lista_Pizze == null || Ordini_Lista == null)
            {
                MessageBox.Show(
                    "❌ Errore di inizializzazione dei controlli.\n\n" +
                    "🔄 Riavviare l'applicazione.",
                    "❌ Errore Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Controllo 3: Verifica che sia stata selezionata una pizza
            if (Lista_Pizze.SelectedItem == null)
            {
                MessageBox.Show(
                    "🍕 Seleziona una pizza dalla lista prima di aggiungerla all'ordine.",
                    "ℹ️ Nessuna Pizza Selezionata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Mette il focus sulla lista delle pizze per guidare l'utente
                Lista_Pizze.Focus();
                return;
            }

            // Controllo 4: Verifica il limite massimo di pizze ordinabili
            if (Ordini_Lista.Items.Count >= MAX_PIZZE_ORDINE)
            {
                MessageBox.Show(
                    $"⚠️ Hai raggiunto il limite massimo di {MAX_PIZZE_ORDINE} pizze per ordine!\n\n" +
                    "🗑️ Per aggiungere altre pizze, rimuovine alcune dall'ordine corrente.",
                    "⚠️ Limite Ordine Raggiunto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Controllo 5: Verifica che la pizza selezionata sia valida
            string pizzaSelezionata = Lista_Pizze.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(pizzaSelezionata))
            {
                MessageBox.Show(
                    "⚠️ La pizza selezionata non è valida.\n\n" +
                    "🍕 Seleziona un'altra pizza dalla lista.",
                    "⚠️ Selezione Non Valida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Aggiunge la pizza all'ordine
            Ordini_Lista.Items.Add(pizzaSelezionata);

            // Aggiorna lo stato dei pulsanti dopo l'operazione
            AggiornaStatoPulsanti();

            // Deseleziona l'item per permettere un nuovo inserimento fluido
            Lista_Pizze.ClearSelected();

            // Mantiene il focus sulla lista pizze per facilitare aggiunte successive
            Lista_Pizze.Focus();
        }

        /// <summary>
        /// Rimuove la pizza selezionata dalla lista ordini.
        /// Esegue controlli per prevenire operazioni non valide e gestisce la selezione post-rimozione.
        /// </summary>
        /// <remarks>
        /// <para><b>Controlli Eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Verifica che il tavolo non sia occupato</item>
        ///   <item>Verifica inizializzazione controllo lista ordini</item>
        ///   <item>Verifica presenza ordini nella lista</item>
        ///   <item>Verifica selezione pizza da rimuovere</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Cancella_Button_Click(object sender, EventArgs e)
        {
            // Controllo 1: Verifica che il tavolo non sia occupato
            if (tavoloOccupato)
            {
                MessageBox.Show(
                    "🍽️ Non puoi modificare l'ordine mentre il tavolo è occupato!\n\n" +
                    "🧹 Sparecchia il tavolo prima di modificare l'ordine.",
                    "⚠️ Tavolo Occupato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Controllo 2: Verifica che il controllo lista ordini sia inizializzato
            if (Ordini_Lista == null)
            {
                MessageBox.Show(
                    "❌ Errore di inizializzazione del controllo ordini.\n\n" +
                    "🔄 Riavviare l'applicazione.",
                    "❌ Errore Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Controllo 3: Verifica che ci siano ordini nella lista
            if (Ordini_Lista.Items.Count == 0)
            {
                MessageBox.Show(
                    "📋 La lista ordini è vuota.\n\n" +
                    "🍕 Aggiungi delle pizze prima di poterne rimuovere.",
                    "ℹ️ Lista Vuota",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Controllo 4: Verifica che sia stata selezionata una pizza da rimuovere
            if (Ordini_Lista.SelectedItem == null)
            {
                MessageBox.Show(
                    "👆 Seleziona una pizza dall'ordine per rimuoverla.",
                    "ℹ️ Nessuna Selezione",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Mette il focus sulla lista ordini per guidare l'utente
                Ordini_Lista.Focus();
                return;
            }

            // Salva l'indice prima della rimozione per gestire la selezione successiva
            int indiceSelezionato = Ordini_Lista.SelectedIndex;

            // Rimuove la pizza selezionata dall'ordine
            Ordini_Lista.Items.Remove(Ordini_Lista.SelectedItem);

            // Gestione intelligente della selezione dopo la rimozione
            if (Ordini_Lista.Items.Count > 0)
            {
                // Seleziona l'elemento nella stessa posizione o l'ultimo se necessario
                int nuovoIndice = Math.Min(indiceSelezionato, Ordini_Lista.Items.Count - 1);
                Ordini_Lista.SelectedIndex = nuovoIndice;
            }

            // Aggiorna lo stato dei pulsanti dopo l'operazione
            AggiornaStatoPulsanti();

            // Mantiene il focus sulla lista ordini per facilitare rimozioni successive
            Ordini_Lista.Focus();
        }

        #endregion

        #region Event Handlers - Gestione Tavolo

        /// <summary>
        /// Gestisce il servizio delle pizze ordinate, visualizzandole sul tavolo.
        /// Esegue controlli approfonditi e gestisce eventuali errori durante il processo.
        /// </summary>
        /// <remarks>
        /// <para><b>Controlli Eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Previene operazioni concorrenti</item>
        ///   <item>Verifica che il tavolo non sia già occupato</item>
        ///   <item>Verifica inizializzazione controlli</item>
        ///   <item>Verifica presenza almeno una pizza nell'ordine</item>
        /// </list>
        /// <para><b>Gestione Errori:</b></para>
        /// <list type="bullet">
        ///   <item>Try-catch per errori durante la disposizione pizze</item>
        ///   <item>Ripristino stato consistente in caso di errore</item>
        ///   <item>Flag servizioInCorso sempre resettato nel finally</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Servi_Button_Click(object sender, EventArgs e)
        {
            // Controllo 1: Previene operazioni concorrenti
            if (servizioInCorso)
            {
                MessageBox.Show(
                    "⏳ Un'operazione di servizio è già in corso.\n\n" +
                    "⏱️ Attendere il completamento.",
                    "⚠️ Operazione in Corso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Controllo 2: Verifica che il tavolo non sia già occupato
            if (tavoloOccupato)
            {
                MessageBox.Show(
                    "🍽️ Il tavolo è già occupato!\n\n" +
                    "🧹 Sparecchia il tavolo prima di servire un nuovo ordine.",
                    "⚠️ Tavolo Occupato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Controllo 3: Verifica che i controlli siano inizializzati
            if (Ordini_Lista == null || Tavolo_Picture == null)
            {
                MessageBox.Show(
                    "❌ Errore di inizializzazione dei controlli.\n\n" +
                    "🔄 Riavviare l'applicazione.",
                    "❌ Errore Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Controllo 4: Verifica che ci sia almeno una pizza nell'ordine
            if (Ordini_Lista.Items.Count == 0)
            {
                MessageBox.Show(
                    "📋 Non puoi servire un ordine vuoto!\n\n" +
                    "🍕 Aggiungi almeno una pizza all'ordine prima di servire.",
                    "⚠️ Ordine Vuoto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                // Mette il focus sulla lista pizze per guidare l'utente
                Lista_Pizze?.Focus();
                return;
            }

            try
            {
                // Imposta il flag di operazione in corso
                servizioInCorso = true;

                // Disabilita i controlli durante il servizio
                DisabilitaControlliOrdine();

                // Cambia la visualizzazione dal pannello ordini a quello del tavolo
                Panel_Ordini.Visible = false;
                Panel_Sparecchia.Visible = true;

                // Pulisce il tavolo da eventuali visualizzazioni precedenti
                Tavolo_Picture.Controls.Clear();

                // Dispone le pizze sul tavolo
                DisposizionaPizzeSulTavolo();

                // Imposta il flag di tavolo occupato
                tavoloOccupato = true;
            }
            catch (Exception ex)
            {
                // Gestione errori: ripristina lo stato in caso di problemi
                MessageBox.Show(
                    $"❌ Si è verificato un errore durante il servizio delle pizze:\n\n{ex.Message}\n\n" +
                    "📋 L'ordine è stato mantenuto. Riprova o contatta l'assistenza.",
                    "❌ Errore Servizio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Ripristina la visualizzazione del pannello ordini
                Panel_Sparecchia.Visible = false;
                Panel_Ordini.Visible = true;
                RiabilitaControlliOrdine();
                AggiornaStatoPulsanti();
            }
            finally
            {
                // Reset del flag di operazione in corso
                servizioInCorso = false;
            }
        }

        /// <summary>
        /// Gestisce la sparecchiatura del tavolo, rimuovendo tutte le pizze visualizzate
        /// e ripristinando il pannello degli ordini per un nuovo ordine.
        /// Richiede conferma all'utente per prevenire cancellazioni accidentali.
        /// </summary>
        /// <remarks>
        /// <para><b>Controlli Eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Verifica che il tavolo sia effettivamente occupato</item>
        ///   <item>Verifica presenza pizze da rimuovere</item>
        ///   <item>Richiesta conferma utente (pulsante No predefinito)</item>
        /// </list>
        /// <para><b>Operazioni Eseguite:</b></para>
        /// <list type="bullet">
        ///   <item>Pulizia lista ordini</item>
        ///   <item>Rimozione e dispose di tutte le PictureBox</item>
        ///   <item>Reset flags stato</item>
        ///   <item>Ripristino pannello ordini</item>
        ///   <item>Reset focus e selezioni</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento</param>
        /// <param name="e">Argomenti dell'evento</param>
        private void Sparecchia_Button_Click(object sender, EventArgs e)
        {
            // Controllo 1: Verifica che il tavolo sia effettivamente occupato
            if (!tavoloOccupato)
            {
                MessageBox.Show(
                    "✅ Il tavolo è già libero!\n\n" +
                    "🧹 Non c'è niente da sparecchiare.",
                    "ℹ️ Tavolo Libero",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Controllo 2: Verifica che ci siano pizze da rimuovere
            if (pizzaPictureBoxes.Count == 0 && Ordini_Lista.Items.Count == 0)
            {
                MessageBox.Show(
                    "📋 Non ci sono ordini da sparecchiare.",
                    "ℹ️ Nessun Ordine",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            // Controllo 3: Richiede conferma prima di procedere
            DialogResult risultato = MessageBox.Show(
                "🧹 Sei sicuro di voler sparecchiare il tavolo?\n\n" +
                "⚠️ L'ordine corrente verrà cancellato definitivamente e dovrai crearne uno nuovo.",
                "❓ Conferma Sparecchiatura",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2  // Il pulsante "No" è quello predefinito
            );

            // Se l'utente annulla, esce dal metodo
            if (risultato != DialogResult.Yes)
            {
                return;
            }

            // Incremento ID ordine a fine ordine
            idOrdineProgressivo++;

            // Esegue la sparecchiatura effettiva
            EseguiSparecchiatura();
        }

        /// <summary>
        /// Esegue la sparecchiatura del tavolo senza richiedere conferma.
        /// Metodo interno utilizzato per i test automatizzati.
        /// </summary>
        internal void EseguiSparecchiatura()
        {
            try
            {
                // Nasconde il pannello di sparecchiatura
                Panel_Sparecchia.Visible = false;

                // Pulisce la lista ordini
                Ordini_Lista?.Items.Clear();

                // Rimuove tutte le PictureBox delle pizze dal tavolo
                RimuoviTutteLePizzeDalTavolo();

                // Resetta il flag di tavolo occupato
                tavoloOccupato = false;

                // Mostra nuovamente il pannello ordini per un nuovo ordine
                Panel_Ordini.Visible = true;

                // Riabilita i controlli e aggiorna lo stato dei pulsanti
                RiabilitaControlliOrdine();
                AggiornaStatoPulsanti();

                // Deseleziona eventuali selezioni residue
                Lista_Pizze?.ClearSelected();
                Ordini_Lista?.ClearSelected();

                // Mette il focus sulla lista pizze per iniziare un nuovo ordine
                Lista_Pizze?.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Si è verificato un errore durante la sparecchiatura:\n\n{ex.Message}\n\n" +
                    "⚠️ Alcune risorse potrebbero non essere state liberate correttamente.",
                    "❌ Errore Sparecchiatura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Tenta comunque di ripristinare uno stato consistente
                tavoloOccupato = false;
                Panel_Ordini.Visible = true;
                Panel_Sparecchia.Visible = false;
                RiabilitaControlliOrdine();
            }
        }

        #endregion

        #region Metodi Privati - Visualizzazione Pizze

        /// <summary>
        /// Dispone tutte le pizze ordinate sul tavolo in una griglia.
        /// Calcola automaticamente la posizione di ogni pizza basandosi sulle costanti di layout.
        /// </summary>
        /// <remarks>
        /// Layout: Le pizze vengono disposte da sinistra a destra, andando a capo quando
        /// si raggiunge il bordo destro del tavolo (1040px di larghezza).
        /// </remarks>
        private void DisposizionaPizzeSulTavolo()
        {
            // Validazione: verifica che ci siano pizze da disporre
            if (Ordini_Lista == null || Ordini_Lista.Items.Count == 0)
            {
                return;
            }

            // Inizializza le coordinate di partenza
            int posizioneX = POSIZIONE_INIZIALE_X;
            int posizioneY = POSIZIONE_INIZIALE_Y;

            // Itera su tutte le pizze nell'ordine
            foreach (var item in Ordini_Lista.Items)
            {
                // Controllo sicurezza: verifica che l'item non sia nullo
                if (item == null)
                {
                    continue;
                }

                // Aggiunge la pizza al tavolo nella posizione calcolata
                AggiungiPizzaAlTavolo(item.ToString(), ref posizioneX, ref posizioneY);
            }
        }

        /// <summary>
        /// Aggiunge una singola pizza al tavolo, creando una PictureBox con l'immagine appropriata.
        /// Gestisce automaticamente il posizionamento su più righe quando necessario.
        /// </summary>
        /// <param name="nomePizza">Nome della pizza da visualizzare</param>
        /// <param name="x">Coordinata X corrente (viene modificata dal metodo)</param>
        /// <param name="y">Coordinata Y corrente (viene modificata dal metodo)</param>
        private void AggiungiPizzaAlTavolo(string nomePizza, ref int x, ref int y)
        {
            // Validazione: verifica che il nome pizza sia valido
            if (string.IsNullOrWhiteSpace(nomePizza))
            {
                return;
            }

            // Validazione: verifica che il controllo tavolo sia inizializzato
            if (Tavolo_Picture == null)
            {
                return;
            }

            // Crea una nuova PictureBox per la pizza
            PictureBox pizzaPictureBox = new PictureBox
            {
                Size = new Size(DIMENSIONE_PIZZA, DIMENSIONE_PIZZA),
                Location = new Point(x, y),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.None
            };

            // Carica l'immagine della pizza
            bool caricamentoRiuscito = CaricaImmaginePizza(pizzaPictureBox, nomePizza);

            // Se il caricamento fallisce, mostra un placeholder visivo
            if (!caricamentoRiuscito)
            {
                pizzaPictureBox.BackColor = Color.LightCoral;

                // Aggiunge una label con il nome della pizza come fallback
                Label lblNomePizza = new Label
                {
                    Text = nomePizza,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    AutoSize = false
                };
                pizzaPictureBox.Controls.Add(lblNomePizza);
            }

            // Aggiunge un tooltip informativo con il nome della pizza
            ToolTip tooltip = new ToolTip
            {
                InitialDelay = 500,
                ReshowDelay = 100
            };
            tooltip.SetToolTip(pizzaPictureBox, $"🍕 Pizza: {nomePizza}");

            // Aggiunge la PictureBox al tavolo e alla lista di tracciamento
            Tavolo_Picture.Controls.Add(pizzaPictureBox);
            pizzaPictureBoxes.Add(pizzaPictureBox);

            // Calcola la posizione successiva per la prossima pizza
            CalcolaPosizioneSuccessiva(ref x, ref y);
        }

        /// <summary>
        /// Carica l'immagine appropriata nella PictureBox della pizza.
        /// Esegue controlli di validazione e gestisce gli errori di caricamento.
        /// </summary>
        /// <param name="pictureBox">PictureBox in cui caricare l'immagine</param>
        /// <param name="nomePizza">Nome della pizza per cui caricare l'immagine</param>
        /// <returns>True se il caricamento è riuscito, False altrimenti</returns>
        private bool CaricaImmaginePizza(PictureBox pictureBox, string nomePizza)
        {
            // Validazione 1: Verifica che i parametri siano validi
            if (pictureBox == null || string.IsNullOrWhiteSpace(nomePizza))
            {
                return false;
            }

            // Validazione 2: Verifica che esista un'immagine configurata per questa pizza
            if (!pizzaImages.TryGetValue(nomePizza, out string percorsoImmagine))
            {
                MessageBox.Show(
                    $"⚠️ Immagine non configurata per la pizza: {nomePizza}\n\n" +
                    "🖼️ Verrà mostrato un placeholder al suo posto.\n" +
                    "📞 Contatta l'amministratore per aggiungere l'immagine.",
                    "⚠️ Configurazione Mancante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            // Validazione 3: Verifica che il file dell'immagine esista fisicamente
            if (!System.IO.File.Exists(percorsoImmagine))
            {
                MessageBox.Show(
                    $"📁 File immagine non trovato:\n{percorsoImmagine}\n\n" +
                    "📂 Assicurati che le immagini siano presenti nella cartella corretta.\n" +
                    "🖼️ Verrà mostrato un placeholder al posto dell'immagine.",
                    "⚠️ File Mancante",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            try
            {
                // Tenta di caricare l'immagine nella PictureBox
                pictureBox.ImageLocation = percorsoImmagine;
                return true;
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(
                    $"💾 Memoria insufficiente per caricare l'immagine: {nomePizza}\n\n" +
                    "🔄 Chiudi alcune applicazioni e riprova.",
                    "❌ Memoria Insufficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show(
                    $"📁 File immagine non trovato per: {nomePizza}\n\n" +
                    "⚠️ Il file potrebbe essere stato spostato o eliminato.",
                    "❌ File Non Trovato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Errore imprevisto durante il caricamento dell'immagine per {nomePizza}:\n\n" +
                    $"{ex.Message}\n\n" +
                    "🖼️ Verrà mostrato un placeholder.",
                    "❌ Errore Caricamento",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }
        }

        /// <summary>
        /// Calcola la posizione successiva per una nuova pizza sul tavolo.
        /// Gestisce automaticamente il passaggio a una nuova riga quando si raggiunge il bordo destro del tavolo.
        /// </summary>
        /// <param name="x">Coordinata X corrente (viene modificata dal metodo)</param>
        /// <param name="y">Coordinata Y corrente (viene modificata dal metodo)</param>
        private void CalcolaPosizioneSuccessiva(ref int x, ref int y)
        {
            // Validazione: verifica che il controllo tavolo sia inizializzato
            if (Tavolo_Picture == null)
            {
                return;
            }

            // Sposta la posizione X per la prossima pizza
            x += DIMENSIONE_PIZZA + SPAZIATURA_PIZZE;

            // Se si supera la larghezza del tavolo, passa alla riga successiva
            if (x + DIMENSIONE_PIZZA > Tavolo_Picture.Width)
            {
                x = POSIZIONE_INIZIALE_X;
                y += DIMENSIONE_PIZZA + SPAZIATURA_VERTICALE;
            }
        }

        /// <summary>
        /// Rimuove tutte le PictureBox delle pizze dal tavolo e libera le risorse.
        /// Pulisce anche la lista di tracciamento delle PictureBox per prevenire memory leak.
        /// </summary>
        private void RimuoviTutteLePizzeDalTavolo()
        {
            // Validazione: verifica che il controllo tavolo sia inizializzato
            if (Tavolo_Picture == null)
            {
                return;
            }

            // Rimuove ogni PictureBox dal controllo tavolo e libera le risorse
            foreach (var pictureBox in pizzaPictureBoxes)
            {
                if (pictureBox != null)
                {
                    // Rimuove dal controllo padre
                    Tavolo_Picture.Controls.Remove(pictureBox);

                    // Libera le risorse grafiche
                    pictureBox.Dispose();
                }
            }

            // Pulisce la lista di tracciamento
            pizzaPictureBoxes.Clear();
        }

        #endregion

        #region Metodi Privati - Gestione Stato UI

        /// <summary>
        /// Aggiorna lo stato di abilitazione di tutti i pulsanti in base allo stato corrente dell'applicazione.
        /// Chiamato dopo ogni operazione che modifica lo stato dell'ordine o del tavolo.
        /// </summary>
        private void AggiornaStatoPulsanti()
        {
            // Validazione: verifica che i controlli siano inizializzati
            if (Ordini_Lista == null)
            {
                return;
            }

            // Pulsante Servi: abilitato solo se ci sono pizze nell'ordine e il tavolo è libero
            if (Servi_Button != null)
            {
                Servi_Button.Enabled = (Ordini_Lista.Items.Count > 0 && !tavoloOccupato);
            }

            // Pulsante Cancella: abilitato solo se c'è una selezione e il tavolo è libero
            if (Cancella_Button != null)
            {
                Cancella_Button.Enabled = (Ordini_Lista.SelectedItem != null && !tavoloOccupato);
            }

            // Pulsante Aggiungi: abilitato se non si è al limite, c'è una selezione e il tavolo è libero
            if (Aggiungi_Button != null && Lista_Pizze != null)
            {
                Aggiungi_Button.Enabled = (Ordini_Lista.Items.Count < MAX_PIZZE_ORDINE &&
                                          Lista_Pizze.SelectedItem != null &&
                                          !tavoloOccupato);
            }
        }

        /// <summary>
        /// Disabilita tutti i controlli del pannello ordini durante il servizio.
        /// Previene modifiche all'ordine mentre è in corso la visualizzazione sul tavolo.
        /// </summary>
        private void DisabilitaControlliOrdine()
        {
            if (Lista_Pizze != null) Lista_Pizze.Enabled = false;
            if (Ordini_Lista != null) Ordini_Lista.Enabled = false;
            if (Aggiungi_Button != null) Aggiungi_Button.Enabled = false;
            if (Cancella_Button != null) Cancella_Button.Enabled = false;
            if (Servi_Button != null) Servi_Button.Enabled = false;
        }

        /// <summary>
        /// Riabilita tutti i controlli del pannello ordini dopo la sparecchiatura.
        /// Ripristina la possibilità di creare un nuovo ordine.
        /// </summary>
        private void RiabilitaControlliOrdine()
        {
            if (Lista_Pizze != null) Lista_Pizze.Enabled = true;
            if (Ordini_Lista != null) Ordini_Lista.Enabled = true;
            if (Aggiungi_Button != null) Aggiungi_Button.Enabled = true;
            if (Cancella_Button != null) Cancella_Button.Enabled = true;
            if (Servi_Button != null) Servi_Button.Enabled = true;
        }

        #endregion

        #region Metodi Pubblici per Test

        /// <summary>
        /// Aggiunge una pizza all'ordine senza interazione utente.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <param name="nomePizza">Nome della pizza da aggiungere</param>
        /// <returns>True se l'aggiunta è riuscita, False altrimenti</returns>
        public bool AggiungiPizzaPerTest(string nomePizza)
        {
            if (tavoloOccupato || Ordini_Lista == null) return false;
            if (Ordini_Lista.Items.Count >= MAX_PIZZE_ORDINE) return false;
            if (string.IsNullOrWhiteSpace(nomePizza)) return false;

            Ordini_Lista.Items.Add(nomePizza);
            AggiornaStatoPulsanti();
            return true;
        }

        /// <summary>
        /// Rimuove una pizza dall'ordine senza interazione utente.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <param name="indice">Indice della pizza da rimuovere</param>
        /// <returns>True se la rimozione è riuscita, False altrimenti</returns>
        public bool RimuoviPizzaPerTest(int indice)
        {
            if (tavoloOccupato || Ordini_Lista == null) return false;
            if (indice < 0 || indice >= Ordini_Lista.Items.Count) return false;

            Ordini_Lista.Items.RemoveAt(indice);
            AggiornaStatoPulsanti();
            return true;
        }

        /// <summary>
        /// Serve l'ordine corrente senza interazione utente.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <returns>True se il servizio è riuscito, False altrimenti</returns>
        public bool ServiOrdinePerTest()
        {
            if (tavoloOccupato || servizioInCorso || Ordini_Lista == null || Tavolo_Picture == null)
                return false;
            if (Ordini_Lista.Items.Count == 0) return false;

            try
            {
                servizioInCorso = true;
                DisabilitaControlliOrdine();
                Panel_Ordini.Visible = false;
                Panel_Sparecchia.Visible = true;
                Tavolo_Picture.Controls.Clear();
                DisposizionaPizzeSulTavolo();
                tavoloOccupato = true;
                return true;
            }
            catch
            {
                Panel_Sparecchia.Visible = false;
                Panel_Ordini.Visible = true;
                RiabilitaControlliOrdine();
                AggiornaStatoPulsanti();
                return false;
            }
            finally
            {
                servizioInCorso = false;
            }
        }

        /// <summary>
        /// Sparecchia il tavolo senza richiedere conferma.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <returns>True se la sparecchiatura è riuscita, False altrimenti</returns>
        public bool SparecchiaPerTest()
        {
            if (!tavoloOccupato) return false;
            EseguiSparecchiatura();
            return true;
        }

        /// <summary>
        /// Seleziona una pizza nella lista pizze disponibili.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <param name="indice">Indice della pizza da selezionare</param>
        /// <returns>True se la selezione è riuscita, False altrimenti</returns>
        public bool SelezionaPizzaPerTest(int indice)
        {
            if (Lista_Pizze == null) return false;
            if (indice < 0 || indice >= Lista_Pizze.Items.Count) return false;
            Lista_Pizze.SelectedIndex = indice;
            return true;
        }

        /// <summary>
        /// Seleziona una pizza nella lista ordini.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <param name="indice">Indice della pizza da selezionare</param>
        /// <returns>True se la selezione è riuscita, False altrimenti</returns>
        public bool SelezionaOrdinePerTest(int indice)
        {
            if (Ordini_Lista == null) return false;
            if (indice < 0 || indice >= Ordini_Lista.Items.Count) return false;
            Ordini_Lista.SelectedIndex = indice;
            return true;
        }

        /// <summary>
        /// Ottiene la posizione di una pizza sul tavolo.
        /// Metodo esposto per permettere i test di posizionamento.
        /// </summary>
        /// <param name="indice">Indice della pizza</param>
        /// <returns>Posizione della pizza, o Point.Empty se non trovata</returns>
        public Point GetPosizionePizzaPerTest(int indice)
        {
            if (indice < 0 || indice >= pizzaPictureBoxes.Count) return Point.Empty;
            return pizzaPictureBoxes[indice].Location;
        }

        /// <summary>
        /// Verifica se una PictureBox mostra un placeholder.
        /// Metodo esposto per permettere i test delle immagini mancanti.
        /// </summary>
        /// <param name="indice">Indice della pizza</param>
        /// <returns>True se mostra un placeholder, False altrimenti</returns>
        public bool IsMostraPlaceholderPerTest(int indice)
        {
            if (indice < 0 || indice >= pizzaPictureBoxes.Count) return false;
            return pizzaPictureBoxes[indice].BackColor == Color.LightCoral;
        }

        #endregion

        #region --- NUOVE FUNZIONALITÀ - Ardente & Taramelli 5^Ci 2025/26 ---

        /// <summary>
        /// Calcola il totale dell'ordine e mostra lo scontrino fiscale a video.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// <para><b>Funzionalità:</b></para>
        /// <list type="bullet">
        ///   <item>Raggruppa le pizze uguali con relative quantità</item>
        ///   <item>Calcola il subtotale per ogni tipo di pizza</item>
        ///   <item>Calcola il totale complessivo dell'ordine</item>
        ///   <item>Genera uno scontrino formattato con intestazione pizzeria</item>
        ///   <item>Visualizza ID ordine progressivo e data/ora</item>
        /// </list>
        /// <para><b>Controlli eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Verifica inizializzazione controllo lista ordini</item>
        ///   <item>Verifica presenza almeno una pizza nell'ordine</item>
        ///   <item>Verifica validità listino prezzi</item>
        ///   <item>Filtraggio elementi nulli dalla lista</item>
        ///   <item>Gestione prezzi mancanti con prezzo default</item>
        ///   <item>Gestione eccezioni durante l'elaborazione</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento (bottone "Conto")</param>
        /// <param name="e">Argomenti dell'evento Click</param>
        private void MostraScontrino_Click(object sender, EventArgs e)
        {
            // =====================================================================
            // CONTROLLO 1: Verifica inizializzazione controllo lista ordini
            // =====================================================================
            if (Ordini_Lista == null)
            {
                MessageBox.Show(
                    "❌ Errore di inizializzazione del controllo ordini.\n\n" +
                    "🔄 Riavviare l'applicazione.",
                    "❌ Errore Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // =====================================================================
            // CONTROLLO 2: Verifica presenza almeno una pizza nell'ordine
            // =====================================================================
            if (Ordini_Lista.Items.Count == 0)
            {
                MessageBox.Show(
                    "📋 Nessun ordine presente per calcolare il conto.\n\n" +
                    "🍕 Aggiungi almeno una pizza all'ordine prima di richiedere lo scontrino.",
                    "⚠️ Ordine Vuoto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // =====================================================================
            // CONTROLLO 3: Verifica validità del listino prezzi
            // =====================================================================
            if (listinoPrezzi == null || listinoPrezzi.Count == 0)
            {
                MessageBox.Show(
                    "❌ Errore: Listino prezzi non disponibile.\n\n" +
                    "📞 Contattare l'amministratore del sistema.",
                    "❌ Errore Configurazione",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                // Inizializzazione variabili per calcolo totale
                decimal totale = 0m;
                StringBuilder sb = new StringBuilder();

                // =================================================================
                // INTESTAZIONE SCONTRINO FISCALE
                // =================================================================
                sb.AppendLine("╔══════════════════════════════════════╗");
                sb.AppendLine("║    🍕 PIZZERIA ARDENTE-TARAMELLI 🍕    ║");
                sb.AppendLine("║           5^Ci - 2025/26              ║");
                sb.AppendLine("╠══════════════════════════════════════╣");
                sb.AppendLine($"║  📅 Data: {DateTime.Now:dd/MM/yyyy}                   ║");
                sb.AppendLine($"║  🕐 Ora:  {DateTime.Now:HH:mm:ss}                      ║");
                sb.AppendLine($"║  🔢 Ordine N.: {idOrdineProgressivo.ToString().PadLeft(4, '0')}                 ║");
                sb.AppendLine("╠══════════════════════════════════════╣");
                sb.AppendLine("║  QTÀ   DESCRIZIONE          IMPORTO  ║");
                sb.AppendLine("╠══════════════════════════════════════╣");

                // =================================================================
                // CONTROLLO 4: Filtra elementi nulli e raggruppa le pizze
                // =================================================================
                var listaPizze = Ordini_Lista.Items
                    .Cast<object>()
                    .Where(item => item != null)
                    .Select(item => item.ToString())
                    .Where(s => !string.IsNullOrWhiteSpace(s));

                // Verifica che ci siano elementi validi dopo il filtraggio
                if (!listaPizze.Any())
                {
                    MessageBox.Show(
                        "⚠️ L'ordine contiene solo elementi non validi.\n\n" +
                        "🗑️ Svuota l'ordine e riprova.",
                        "❌ Ordine Non Valido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Raggruppa le pizze per tipo
                var pizzeRaggruppate = listaPizze.GroupBy(p => p);

                // =================================================================
                // ELABORAZIONE RIGHE SCONTRINO
                // =================================================================
                foreach (var gruppo in pizzeRaggruppate)
                {
                    string nomePizza = gruppo.Key;
                    int quantita = gruppo.Count();

                    // CONTROLLO 5: Gestione prezzo mancante nel listino
                    decimal prezzoUnitario;
                    if (listinoPrezzi.ContainsKey(nomePizza))
                    {
                        prezzoUnitario = listinoPrezzi[nomePizza];
                    }
                    else
                    {
                        // Prezzo predefinito per pizze non configurate
                        prezzoUnitario = 6.00m;

                        // Log dell'anomalia per debug
                        System.Diagnostics.Debug.WriteLine(
                            $"[WARNING] Prezzo non trovato per '{nomePizza}'. Applicato prezzo default: € 6,00"
                        );
                    }

                    // Calcolo subtotale riga
                    decimal subtotaleRiga = prezzoUnitario * quantita;
                    totale += subtotaleRiga;

                    // Formattazione riga scontrino
                    string rigaFormattata = $"║  {quantita}x   {nomePizza.PadRight(18)} € {subtotaleRiga,7:F2} ║";
                    sb.AppendLine(rigaFormattata);
                }

                // =================================================================
                // FOOTER SCONTRINO CON TOTALE
                // =================================================================
                sb.AppendLine("╠══════════════════════════════════════╣");
                sb.AppendLine($"║  💰 TOTALE ORDINE:          € {totale,7:F2} ║");
                sb.AppendLine("╠══════════════════════════════════════╣");
                sb.AppendLine("║                                      ║");
                sb.AppendLine("║     😊 Grazie per averci scelto! 😊    ║");
                sb.AppendLine("║        Ardente & Taramelli           ║");
                sb.AppendLine("║            5^Ci - 2025/26            ║");
                sb.AppendLine("╚══════════════════════════════════════╝");

                // =================================================================
                // VISUALIZZAZIONE SCONTRINO
                // =================================================================
                MessageBox.Show(
                    sb.ToString(),
                    $"🧾 Scontrino Fiscale - Ordine #{idOrdineProgressivo:0000}",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (InvalidCastException ex)
            {
                // CONTROLLO 6: Gestione errori di cast
                MessageBox.Show(
                    $"⚠️ Errore durante l'elaborazione degli elementi dell'ordine:\n\n" +
                    $"❌ {ex.Message}\n\n" +
                    "ℹ️ Alcuni elementi potrebbero non essere nel formato corretto.",
                    "❌ Errore Elaborazione",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                // Gestione errori generici
                MessageBox.Show(
                    $"⚠️ Si è verificato un errore imprevisto durante la generazione dello scontrino:\n\n" +
                    $"❌ {ex.Message}\n\n" +
                    "📞 Contattare l'assistenza tecnica.",
                    "❌ Errore Scontrino",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        /// <summary>
        /// Salva i dettagli dell'ordine corrente su un file di testo per lo storico.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// <para><b>Formato salvataggio:</b></para>
        /// <code>
        /// ID: 0001 | DATA/ORA: 15/01/2026 14:30:25 | ORDINE: 2x Margherita, 1x Capricciosa
        /// </code>
        /// <para><b>Controlli eseguiti:</b></para>
        /// <list type="number">
        ///   <item>Verifica inizializzazione controllo lista ordini</item>
        ///   <item>Verifica presenza almeno una pizza nell'ordine</item>
        ///   <item>Verifica validità percorso file</item>
        ///   <item>Verifica permessi di scrittura</item>
        ///   <item>Filtraggio e validazione elementi lista</item>
        ///   <item>Verifica file non bloccato</item>
        ///   <item>Gestione eccezioni I/O multiple</item>
        /// </list>
        /// </remarks>
        /// <param name="sender">Oggetto che ha generato l'evento (bottone "Salva Storico")</param>
        /// <param name="e">Argomenti dell'evento Click</param>
        private void SalvaStorico_Click(object sender, EventArgs e)
        {
            // Nome del file per lo storico ordini
            const string NOME_FILE_STORICO = "StoricoOrdini.txt";

            // =====================================================================
            // CONTROLLO 1: Verifica inizializzazione controllo lista ordini
            // =====================================================================
            if (Ordini_Lista == null)
            {
                MessageBox.Show(
                    "❌ Errore di inizializzazione del controllo ordini.\n\n" +
                    "🔄 Riavviare l'applicazione.",
                    "❌ Errore Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // =====================================================================
            // CONTROLLO 2: Verifica presenza almeno una pizza nell'ordine
            // =====================================================================
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

            // =====================================================================
            // CONTROLLO 3: Verifica validità percorso file
            // =====================================================================
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

            // =====================================================================
            // CONTROLLO 4: Verifica permessi di scrittura nella directory
            // =====================================================================
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
                // =================================================================
                // CONTROLLO 5: Filtra e valida elementi della lista
                // =================================================================
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

                // =================================================================
                // PREPARAZIONE DATI PER IL SALVATAGGIO
                // =================================================================
                var dettaglioPizze = elementiValidi
                    .GroupBy(p => p)
                    .Select(g => $"{g.Count()}x {g.Key}");

                string stringaPizze = string.Join(", ", dettaglioPizze);

                // Costruisce la riga di log formattata
                StringBuilder logLine = new StringBuilder();
                logLine.Append($"ID: {idOrdineProgressivo:0000} | ");
                logLine.Append($"DATA/ORA: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | ");
                logLine.Append($"ORDINE: {stringaPizze}");

                // =================================================================
                // CONTROLLO 6: Verifica file non bloccato da altro processo
                // =================================================================
                if (File.Exists(percorsoCompleto))
                {
                    try
                    {
                        using (FileStream fs = File.Open(percorsoCompleto, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                        {
                            // File accessibile, chiude immediatamente
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(
                            "🔒 Il file dello storico è attualmente in uso da un altro programma.\n\n" +
                            "📁 Chiudere l'applicazione che sta utilizzando il file e riprovare.",
                            "⚠️ File in Uso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                // =================================================================
                // SCRITTURA SU FILE (MODALITÀ APPEND)
                // =================================================================
                File.AppendAllText(
                    percorsoCompleto,
                    logLine.ToString() + Environment.NewLine,
                    Encoding.UTF8
                );

                // =================================================================
                // CONFERMA OPERAZIONE RIUSCITA
                // =================================================================
                MessageBox.Show(
                    $"✅ Ordine #{idOrdineProgressivo:0000} salvato correttamente nello storico!\n\n" +
                    $"📁 File: {NOME_FILE_STORICO}\n" +
                    $"📂 Percorso: {percorsoCompleto}\n\n" +
                    $"👥 Ardente & Taramelli - 5^Ci - 2025/26",
                    "💾 Salvataggio Completato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            // =====================================================================
            // CONTROLLO 7: Gestione eccezioni I/O dettagliate
            // =====================================================================
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

        #endregion

        #region Metodi Pubblici per Test - Nuove Funzionalità

        /// <summary>
        /// Genera lo scontrino senza visualizzazione grafica.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <returns>
        /// Tupla contenente:
        /// - success: True se la generazione è riuscita
        /// - totale: Importo totale calcolato
        /// - dettaglio: Stringa con il dettaglio dello scontrino
        /// </returns>
        public (bool success, decimal totale, string dettaglio) GeneraScontrinoPerTest()
        {
            if (Ordini_Lista == null || Ordini_Lista.Items.Count == 0)
            {
                return (false, 0m, "Ordine vuoto");
            }

            if (listinoPrezzi == null || listinoPrezzi.Count == 0)
            {
                return (false, 0m, "Listino prezzi non disponibile");
            }

            try
            {
                decimal totale = 0m;
                StringBuilder sb = new StringBuilder();

                var listaPizze = Ordini_Lista.Items
                    .Cast<object>()
                    .Where(item => item != null)
                    .Select(item => item.ToString())
                    .Where(s => !string.IsNullOrWhiteSpace(s));

                if (!listaPizze.Any())
                {
                    return (false, 0m, "Nessun elemento valido nell'ordine");
                }

                var pizzeRaggruppate = listaPizze.GroupBy(p => p);

                foreach (var gruppo in pizzeRaggruppate)
                {
                    string nomePizza = gruppo.Key;
                    int quantita = gruppo.Count();
                    decimal prezzoUnitario = listinoPrezzi.ContainsKey(nomePizza)
                        ? listinoPrezzi[nomePizza]
                        : 6.00m;
                    decimal subtotale = prezzoUnitario * quantita;
                    totale += subtotale;
                    sb.AppendLine($"{quantita}x {nomePizza}: € {subtotale:F2}");
                }

                sb.AppendLine($"TOTALE: € {totale:F2}");

                return (true, totale, sb.ToString());
            }
            catch (Exception ex)
            {
                return (false, 0m, $"Errore: {ex.Message}");
            }
        }

        /// <summary>
        /// Salva lo storico ordini senza visualizzazione messaggi.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <param name="percorsoFile">Percorso del file dove salvare (opzionale)</param>
        /// <returns>
        /// Tupla contenente:
        /// - success: True se il salvataggio è riuscito
        /// - messaggio: Messaggio descrittivo dell'esito
        /// </returns>
        public (bool success, string messaggio) SalvaStoricoPerTest(string percorsoFile = null)
        {
            if (Ordini_Lista == null || Ordini_Lista.Items.Count == 0)
            {
                return (false, "Ordine vuoto");
            }

            string nomeFile = percorsoFile ?? "StoricoOrdini_Test.txt";

            try
            {
                var elementiValidi = Ordini_Lista.Items
                    .Cast<object>()
                    .Where(item => item != null)
                    .Select(item => item.ToString())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();

                if (elementiValidi.Count == 0)
                {
                    return (false, "Nessun elemento valido");
                }

                var dettaglioPizze = elementiValidi
                    .GroupBy(p => p)
                    .Select(g => $"{g.Count()}x {g.Key}");

                string stringaPizze = string.Join(", ", dettaglioPizze);

                string logLine = $"ID: {idOrdineProgressivo:0000} | " +
                                $"DATA/ORA: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | " +
                                $"ORDINE: {stringaPizze}";

                File.AppendAllText(nomeFile, logLine + Environment.NewLine, Encoding.UTF8);

                return (true, $"Salvato su {nomeFile}");
            }
            catch (Exception ex)
            {
                return (false, $"Errore: {ex.Message}");
            }
        }

        /// <summary>
        /// Ottiene il prezzo di una pizza dal listino.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <param name="nomePizza">Nome della pizza da cercare</param>
        /// <returns>Prezzo della pizza, o -1 se non trovata</returns>
        public decimal GetPrezzoPizzaPerTest(string nomePizza)
        {
            if (string.IsNullOrWhiteSpace(nomePizza)) return -1m;
            if (listinoPrezzi.ContainsKey(nomePizza)) return listinoPrezzi[nomePizza];
            return -1m;
        }

        /// <summary>
        /// Ottiene l'ID ordine progressivo corrente.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <returns>ID ordine progressivo corrente</returns>
        public int GetIdOrdineProgressivoPerTest()
        {
            return idOrdineProgressivo;
        }

        /// <summary>
        /// Verifica l'esistenza del file storico.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <param name="percorsoFile">Percorso del file da verificare</param>
        /// <returns>True se il file esiste, False altrimenti</returns>
        public bool VerificaEsistenzaFileStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
        {
            return File.Exists(percorsoFile);
        }

        /// <summary>
        /// Legge l'ultima riga del file storico.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <param name="percorsoFile">Percorso del file da leggere</param>
        /// <returns>Ultima riga del file, o stringa vuota se errore</returns>
        public string LeggiUltimaRigaStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
        {
            try
            {
                if (!File.Exists(percorsoFile)) return string.Empty;
                var righe = File.ReadAllLines(percorsoFile, Encoding.UTF8);
                return righe.Length > 0 ? righe[righe.Length - 1] : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Conta il numero di righe nel file storico.
        /// Metodo esposto per permettere i test automatizzati.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - Classe 5^Ci - A.S. 2025/26</para>
        /// </remarks>
        /// <param name="percorsoFile">Percorso del file da analizzare</param>
        /// <returns>Numero di righe nel file, o -1 se errore</returns>
        public int ContaRigheStoricoPerTest(string percorsoFile = "StoricoOrdini.txt")
        {
            try
            {
                if (!File.Exists(percorsoFile)) return 0;
                return File.ReadAllLines(percorsoFile, Encoding.UTF8).Length;
            }
            catch
            {
                return -1;
            }
        }

        #endregion
    }
}
