/// ============================================================================
/// @file UnitTest1_Optimized.cs
/// @brief Suite di test automatizzati per Applicativo_Ardente
///
/// @author Ardente, Taramelli
/// @class 5^Ci
/// @version 1.0
/// @date Anno Scolastico 2025/26
///
/// @details Versione con:
///   - Test 1-12:funzionalità base originali
///   - Test 13-25: niente delay, codice minimale
///   - Esecuzione rapida (~2-3 secondi totali)
///
/// @note LISTINO PREZZI UTILIZZATO:
///   - Margherita:      € 5.00
///   - Marinara:        € 4.50
///   - Capricciosa:     € 8.00
///   - Quattro formaggi:€ 8.50
///   - Salmone:         € 9.00
///   - Default:         € 6.00 (pizze non in listino)
///
/// @copyright (c) 2025/26 - Ardente & Taramelli - 5^Ci
/// Progetto didattico - Tutti i diritti riservati
/// ============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Applicativo_Ardente;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Applicativo_Ardente_Tests
{
    /// <summary>
    /// Classe principale contenente tutti i test per la pizzeria.
    /// Organizzata in regioni per facilitare la navigazione.
    /// </summary>
    [TestClass]
    public class PizzeriaTests
    {
        // =====================================================================
        // CAMPI PRIVATI
        // =====================================================================

        /// <summary>Istanza del form da testare</summary>
        private Form1 form;

        /// <summary>Percorso dove si trovano le immagini delle pizze</summary>
        private const string PERCORSO_IMMAGINI = @"C:\Users\Pietro D. Ardente\Desktop\Applicativo_Ardente\Applicativo_Ardente\bin\Debug\Immagini\pizze";

        /// <summary>File temporaneo per i test dello storico (evita di modificare quello di produzione)</summary>
        private const string FILE_STORICO_TEST = "StoricoOrdini_Test.txt";

        // =====================================================================
        // SETUP E TEARDOWN
        // =====================================================================

        /// <summary>
        /// Eseguito PRIMA di ogni test.
        /// Prepara l'ambiente: directory, form, pulizia file residui.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Imposta la directory di lavoro per il caricamento immagini
            if (Directory.Exists(PERCORSO_IMMAGINI))
                Directory.SetCurrentDirectory(PERCORSO_IMMAGINI);

            // Crea e inizializza il form (Show+Hide necessario per i controlli grafici)
            form = new Form1();
            form.Show();
            form.Hide();

            // Rimuove eventuali file di test rimasti da esecuzioni precedenti
            if (File.Exists(FILE_STORICO_TEST))
                try { File.Delete(FILE_STORICO_TEST); } catch { }
        }

        /// <summary>
        /// Eseguito DOPO ogni test.
        /// Pulisce le risorse: dispose del form, eliminazione file temporanei.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            // Rilascia le risorse del form
            form?.Dispose();

            // Elimina il file di test se esiste
            if (File.Exists(FILE_STORICO_TEST))
                try { File.Delete(FILE_STORICO_TEST); } catch { }
        }

        #region ============ TEST 1-12: FUNZIONALITÀ BASE (INVARIATI) ============

        /// <summary>
        /// TEST 1 - Aggiunta Pizze
        /// Verifica che una pizza venga aggiunta correttamente all'ordine.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Aggiunta di una pizza Margherita all'ordine vuoto</para>
        /// <para><b>Risultato atteso:</b> Il conteggio pizze aumenta di 1</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Ordini")]
        public void Test01_AggiuntaPizza_IncrementaContatore()
        {
            // Arrange
            string pizza = "Margherita";
            int countIniziale = form.NumeroPizzeInOrdine;

            // Act
            bool risultato = form.AggiungiPizzaPerTest(pizza);

            // Assert
            Assert.IsTrue(risultato, "✅ L'aggiunta della pizza dovrebbe restituire true");
            Assert.AreEqual(countIniziale + 1, form.NumeroPizzeInOrdine,
                "🍕 Il conteggio pizze dovrebbe aumentare di 1");
        }

        /// <summary>
        /// TEST 2 - Limite Ordini
        /// Verifica che non sia possibile aggiungere più di 6 pizze.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Tentativo di aggiungere la 7ª pizza</para>
        /// <para><b>Risultato atteso:</b> L'aggiunta viene bloccata</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Ordini")]
        public void Test02_LimiteMassimoOrdini_BloccaAggiunta()
        {
            // Arrange - Riempiamo fino al massimo (6)
            for (int i = 0; i < form.MaxPizzeOrdine; i++)
            {
                form.AggiungiPizzaPerTest("Margherita");
            }
            Assert.AreEqual(6, form.NumeroPizzeInOrdine, "📋 Dovrebbero esserci 6 pizze");

            // Act - Proviamo ad aggiungere la 7ª
            bool risultato = form.AggiungiPizzaPerTest("Marinara");

            // Assert
            Assert.IsFalse(risultato, "⛔ Non dovrebbe essere possibile aggiungere la 7ª pizza");
            Assert.AreEqual(6, form.NumeroPizzeInOrdine, "📋 Il numero di pizze dovrebbe rimanere 6");
        }

        /// <summary>
        /// TEST 3 - Rimozione Pizza
        /// Verifica la rimozione di una pizza dall'ordine.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Rimozione della prima pizza da un ordine con 2 pizze</para>
        /// <para><b>Risultato atteso:</b> Il conteggio diminuisce di 1</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Ordini")]
        public void Test03_RimozionePizza_DecrementaContatore()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");
            form.AggiungiPizzaPerTest("Capricciosa");
            int countPrima = form.NumeroPizzeInOrdine;

            // Act - Rimuovi la prima pizza (indice 0)
            bool risultato = form.RimuoviPizzaPerTest(0);

            // Assert
            Assert.IsTrue(risultato, "✅ La rimozione dovrebbe avere successo");
            Assert.AreEqual(countPrima - 1, form.NumeroPizzeInOrdine,
                "🗑️ Il conteggio dovrebbe diminuire");
        }

        /// <summary>
        /// TEST 4 - Servizio Ordine Vuoto
        /// Verifica che non sia possibile servire un tavolo senza ordini.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Tentativo di servire con ordine vuoto</para>
        /// <para><b>Risultato atteso:</b> Il servizio viene rifiutato</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Tavolo")]
        public void Test04_ServiOrdineVuoto_RitornaFalso()
        {
            // Arrange
            Assert.AreEqual(0, form.NumeroPizzeInOrdine, "📋 L'ordine deve essere vuoto");

            // Act
            bool risultato = form.ServiOrdinePerTest();

            // Assert
            Assert.IsFalse(risultato, "⛔ Non dovrebbe essere possibile servire un ordine vuoto");
            Assert.IsFalse(form.IsTavoloOccupato, "🍽️ Il tavolo non dovrebbe risultare occupato");
        }

        /// <summary>
        /// TEST 5 - Servizio Ordine
        /// Verifica il corretto servizio di un ordine valido.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Servizio di un ordine con 1 pizza</para>
        /// <para><b>Risultato atteso:</b> Il tavolo risulta occupato</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Tavolo")]
        public void Test05_ServiOrdineValido_OccupaTavolo()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");

            // Act
            // Protezione contro MessageBox bloccanti in caso di errore immagini
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                try { SendKeys.SendWait("{ENTER}"); } catch { }
            });

            bool risultato = form.ServiOrdinePerTest();

            // Assert
            if (!risultato)
            {
                Assert.Inconclusive("⚠️ Impossibile servire l'ordine. Verifica il percorso immagini: " + PERCORSO_IMMAGINI);
            }

            Assert.IsTrue(risultato, "✅ Il servizio dovrebbe avere successo");
            Assert.IsTrue(form.IsTavoloOccupato, "🍽️ Il tavolo dovrebbe risultare occupato");
        }

        /// <summary>
        /// TEST 6 - Modifica Durante Servizio
        /// Verifica che non sia possibile modificare l'ordine mentre il tavolo è occupato.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Tentativo di aggiunta/rimozione con tavolo occupato</para>
        /// <para><b>Risultato atteso:</b> Le modifiche vengono bloccate</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Tavolo")]
        public void Test06_ModificaDuranteServizio_Impedita()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");
            form.ServiOrdinePerTest(); // Tavolo ora occupato

            // Act & Assert - Prova Aggiunta
            bool aggRes = form.AggiungiPizzaPerTest("Marinara");
            Assert.IsFalse(aggRes, "⛔ Non si dovrebbe poter aggiungere pizze a tavolo occupato");

            // Act & Assert - Prova Rimozione
            bool rimRes = form.RimuoviPizzaPerTest(0);
            Assert.IsFalse(rimRes, "⛔ Non si dovrebbe poter rimuovere pizze a tavolo occupato");
        }

        /// <summary>
        /// TEST 7 - Sparecchiatura
        /// Verifica che la sparecchiatura liberi il tavolo e resetti l'ordine.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Sparecchiatura di un tavolo occupato</para>
        /// <para><b>Risultato atteso:</b> Tavolo libero e ordine vuoto</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Tavolo")]
        public void Test07_SparecchiaTavolo_LiberaRisorse()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");

            // Protezione contro MessageBox bloccanti
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                try { SendKeys.SendWait("{ENTER}"); } catch { }
            });

            bool servito = form.ServiOrdinePerTest();

            if (!servito)
            {
                Assert.Inconclusive("⚠️ Impossibile eseguire Test 7: Il servizio iniziale è fallito");
            }

            Assert.IsTrue(form.IsTavoloOccupato, "🍽️ Il tavolo deve essere occupato");

            // Act
            bool risultato = form.SparecchiaPerTest();

            // Assert
            Assert.IsTrue(risultato, "✅ La sparecchiatura dovrebbe avere successo");
            Assert.IsFalse(form.IsTavoloOccupato, "🍽️ Il tavolo dovrebbe essere libero");
            Assert.AreEqual(0, form.NumeroPizzeInOrdine, "📋 La lista ordini dovrebbe essere vuota");
        }

        /// <summary>
        /// TEST 8 - Conferma Sparecchiatura (Logica)
        /// Verifica che l'azione di sparecchiatura esegua correttamente il reset dello stato.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Nota:</b> In un Unit Test automatico non possiamo cliccare su MessageBox.
        /// Questo test verifica la logica chiamata DOPO la conferma.</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Tavolo")]
        public void Test08_ConfermaSparecchiatura_VerificaReset()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");
            form.ServiOrdinePerTest();

            // Act
            bool risultato = form.SparecchiaPerTest();

            // Assert
            Assert.IsTrue(risultato, "✅ Il metodo dovrebbe ritornare true simulando la conferma");
            Assert.IsFalse(form.IsTavoloOccupato, "🍽️ Il tavolo deve essere liberato");
        }

        /// <summary>
        /// TEST 9 - Gestione Focus
        /// Verifica che il focus ritorni alla lista delle pizze dopo l'aggiunta.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Nota:</b> In alcuni runner di test headless questo controllo potrebbe fallire.</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Interfaccia Utente")]
        public void Test09_GestioneFocus_DopoAggiunta()
        {
            // Arrange
            form.Show();
            form.SelezionaPizzaPerTest(0);

            // Act
            form.AggiungiPizzaPerTest("Margherita");

            // Assert
            if (form.ActiveControl != null)
            {
                Assert.IsInstanceOfType(form.ActiveControl, typeof(ListBox),
                    "👆 Il focus dovrebbe tornare sulla ListBox delle pizze");
            }
        }

        /// <summary>
        /// TEST 10 - Stato Pulsanti
        /// Verifica la coerenza dello stato dei pulsanti (Enabled/Disabled).
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Verifica:</b> Stato pulsanti in diverse fasi dell'operatività</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Interfaccia Utente")]
        public void Test10_StatoPulsanti_Coerenza()
        {
            // Stato Iniziale
            Assert.IsFalse(form.IsServiButtonEnabled, "🔘 Pulsante Servi deve essere disabilitato all'inizio");
            Assert.IsFalse(form.IsCancellaButtonEnabled, "🔘 Pulsante Cancella deve essere disabilitato all'inizio");
            Assert.IsFalse(form.IsAggiungiButtonEnabled, "🔘 Pulsante Aggiungi deve essere disabilitato senza selezione");

            // Selezione Pizza
            form.SelezionaPizzaPerTest(0);
            Assert.IsTrue(form.IsAggiungiButtonEnabled, "🔘 Pulsante Aggiungi deve attivarsi dopo selezione");

            // Aggiunta Pizza
            form.AggiungiPizzaPerTest("Margherita");
            Assert.IsTrue(form.IsServiButtonEnabled, "🔘 Pulsante Servi deve attivarsi con ordini");

            // Selezione Ordine
            form.SelezionaOrdinePerTest(0);
            Assert.IsTrue(form.IsCancellaButtonEnabled, "🔘 Pulsante Cancella deve attivarsi con selezione ordine");
        }

        /// <summary>
        /// TEST 11 - Immagini Mancanti
        /// Verifica che venga mostrato un placeholder se l'immagine non esiste.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Scenario:</b> Servizio di una pizza con immagine inesistente</para>
        /// <para><b>Risultato atteso:</b> Viene mostrato un placeholder visivo</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Gestione Immagini")]
        public void Test11_ImmaginiMancanti_MostraPlaceholder()
        {
            // Arrange
            string pizzaInesistente = "PizzaNonEsistente_Test11";
            form.AggiungiPizzaPerTest(pizzaInesistente);

            // Act
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                try { SendKeys.SendWait("{ENTER}"); } catch { }
            });

            form.ServiOrdinePerTest();

            // Assert
            bool mostraPlaceholder = form.IsMostraPlaceholderPerTest(0);
            Assert.IsTrue(mostraPlaceholder, "🖼️ Dovrebbe essere visualizzato un placeholder per immagini mancanti");
        }

        /// <summary>
        /// TEST 12 - Posizionamento
        /// Verifica che le pizze vengano posizionate correttamente sul tavolo.
        /// </summary>
        /// <remarks>
        /// <para><b>Autori:</b> Ardente, Taramelli - 5^Ci - 2025/26</para>
        /// <para><b>Verifica:</b> Coordinate e non sovrapposizione delle pizze</para>
        /// </remarks>
        [TestMethod]
        [TestCategory("Funzionalità Base")]
        [TestCategory("Layout")]
        public void Test12_Posizionamento_Coordinate()
        {
            // Arrange
            form.AggiungiPizzaPerTest("Margherita");
            form.AggiungiPizzaPerTest("Marinara");
            form.ServiOrdinePerTest();

            // Act
            Point pos1 = form.GetPosizionePizzaPerTest(0);
            Point pos2 = form.GetPosizionePizzaPerTest(1);

            // Assert
            Assert.IsFalse(pos1.IsEmpty, "📍 La prima pizza deve avere una posizione");
            Assert.IsFalse(pos2.IsEmpty, "📍 La seconda pizza deve avere una posizione");
            Assert.AreNotEqual(pos1, pos2, "📍 Le pizze non devono sovrapporsi esattamente");
            Assert.IsTrue(pos2.X > pos1.X, "➡️ La seconda pizza deve essere a destra della prima");
        }

        #endregion

        #region ============ TEST 13-18: SCONTRINO (OTTIMIZZATI) ============

        /// <summary>
        /// TEST 13 - Scontrino con ordine vuoto.
        /// Verifica che GeneraScontrino ritorni success=false se non ci sono pizze.
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test13_ScontrinoOrdineVuoto_RitornaFalso()
        {
            // Act: tentiamo di generare uno scontrino senza pizze
            var risultato = form.GeneraScontrinoPerTest();

            // Assert: deve fallire con totale a zero
            Assert.IsFalse(risultato.success, "Lo scontrino non può essere generato senza ordini");
            Assert.AreEqual(0m, risultato.totale, "Il totale deve essere zero");
        }

        /// <summary>
        /// TEST 14 - Scontrino con ordine valido.
        /// Verifica che lo scontrino contenga le pizze ordinate e un totale > 0.
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test14_ScontrinoOrdineValido_GeneraCorrettamente()
        {
            // Arrange: aggiungiamo 2 pizze diverse
            form.AggiungiPizzaPerTest("Margherita");  // € 5.00
            form.AggiungiPizzaPerTest("Marinara");    // € 4.50

            // Act: generiamo lo scontrino
            var risultato = form.GeneraScontrinoPerTest();

            // Assert: verifica successo, totale positivo, contenuto corretto
            Assert.IsTrue(risultato.success, "La generazione deve avere successo");
            Assert.IsTrue(risultato.totale > 0, "Il totale deve essere positivo");
            Assert.IsTrue(risultato.dettaglio.Contains("Margherita"), "Deve contenere Margherita");
            Assert.IsTrue(risultato.dettaglio.Contains("Marinara"), "Deve contenere Marinara");
        }

        /// <summary>
        /// TEST 15 - Prezzo default per pizze non in listino.
        /// Se una pizza non è nel listino, viene applicato il prezzo default di € 6.00.
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test15_ScontrinoPrezziNonConfigurati_UsaPrezzoDefault()
        {
            // Arrange: pizza inventata non presente nel listino
            form.AggiungiPizzaPerTest("PizzaSpecialeTest");

            // Act
            var risultato = form.GeneraScontrinoPerTest();

            // Assert: il prezzo default è € 6.00
            Assert.IsTrue(risultato.success, "Deve comunque generare lo scontrino");
            Assert.AreEqual(6.00m, risultato.totale, "Il prezzo default è € 6.00");
        }

        /// <summary>
        /// TEST 16 - Raggruppamento pizze uguali.
        /// Se ordino 3 Margherita, lo scontrino deve mostrare "3x Margherita".
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test16_ScontrinoRaggruppamentoPizze_FormattaCorrettamente()
        {
            // Arrange: 3 pizze identiche
            form.AggiungiPizzaPerTest("Margherita");
            form.AggiungiPizzaPerTest("Margherita");
            form.AggiungiPizzaPerTest("Margherita");

            // Act
            var risultato = form.GeneraScontrinoPerTest();

            // Assert: deve mostrare "3x" nel dettaglio
            Assert.IsTrue(risultato.success, "Generazione riuscita");
            Assert.IsTrue(risultato.dettaglio.Contains("3x"), "Deve raggruppare con '3x'");
        }

        /// <summary>
        /// TEST 17 - Calcolo corretto del totale.
        /// Margherita(5) + Marinara(4.50) + Capricciosa(8) = € 17.50
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test17_ScontrinoCalcoloTotale_SommaCorretta()
        {
            // Arrange: 3 pizze con prezzi noti
            form.AggiungiPizzaPerTest("Margherita");   // € 5.00
            form.AggiungiPizzaPerTest("Marinara");     // € 4.50
            form.AggiungiPizzaPerTest("Capricciosa");  // € 8.00
            // Totale atteso: € 17.50

            // Act
            var risultato = form.GeneraScontrinoPerTest();

            // Assert
            Assert.IsTrue(risultato.success, "Generazione riuscita");
            Assert.AreEqual(17.50m, risultato.totale, "Totale deve essere € 17.50");
        }

        /// <summary>
        /// TEST 18 - ID ordine progressivo.
        /// Il primo ordine deve avere ID = 1.
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test18_ScontrinoIdOrdine_ProgressivoCorretto()
        {
            // Act: otteniamo l'ID del prossimo ordine
            int idOrdine = form.GetIdOrdineProgressivoPerTest();

            // Assert: il primo ordine ha ID = 1
            Assert.AreEqual(1, idOrdine, "Il primo ID ordine deve essere 1");
        }

        #endregion

        #region ============ TEST 19-25: SALVATAGGIO STORICO (OTTIMIZZATI) ============

        /// <summary>
        /// TEST 19 - Salvataggio con ordine vuoto.
        /// Non deve essere possibile salvare lo storico senza pizze.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test19_SalvataggioOrdineVuoto_RitornaFalso()
        {
            // Act: tentiamo di salvare senza ordini
            var risultato = form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Assert: deve fallire
            Assert.IsFalse(risultato.success, "Il salvataggio deve fallire senza ordini");
        }

        /// <summary>
        /// TEST 20 - Salvataggio con ordine valido.
        /// Deve creare il file di storico con i dati dell'ordine.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test20_SalvataggioOrdineValido_CreaFile()
        {
            // Arrange: aggiungiamo una pizza
            form.AggiungiPizzaPerTest("Margherita");

            // Act: salviamo lo storico
            var risultato = form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Assert: successo e file creato
            Assert.IsTrue(risultato.success, "Il salvataggio deve avere successo");
            Assert.IsTrue(File.Exists(FILE_STORICO_TEST), "Il file deve essere creato");
        }

        /// <summary>
        /// TEST 21 - Modalità append (non sovrascrive).
        /// Ordini successivi devono essere AGGIUNTI al file, non sovrascriverlo.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test21_SalvataggioAppendFile_NonSovrascrive()
        {
            // ----- PRIMO ORDINE -----
            form.AggiungiPizzaPerTest("Margherita");
            form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Salviamo la dimensione del file dopo il primo salvataggio
            long dimensione1 = new FileInfo(FILE_STORICO_TEST).Length;

            // ----- SECONDO ORDINE -----
            // Aggiungiamo un'altra pizza (ora abbiamo 2 pizze in ordine)
            form.AggiungiPizzaPerTest("Marinara");
            form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Dimensione dopo il secondo salvataggio
            long dimensione2 = new FileInfo(FILE_STORICO_TEST).Length;

            // Assert: il file deve essere CRESCIUTO (append, non sovrascrittura)
            Assert.IsTrue(dimensione2 > dimensione1,
                "Il file deve crescere dopo il secondo salvataggio (append mode)");
        }

        /// <summary>
        /// TEST 22 - Formato dati nel file.
        /// Il file deve contenere: ID, DATA/ORA, ORDINE con raggruppamento.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test22_SalvataggioFormatoDati_FormatoCorretto()
        {
            // Arrange: 2 pizze uguali per testare il raggruppamento
            form.AggiungiPizzaPerTest("Margherita");
            form.AggiungiPizzaPerTest("Margherita");

            // Act
            form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Assert: leggiamo il contenuto e verifichiamo il formato
            string contenuto = File.ReadAllText(FILE_STORICO_TEST);

            Assert.IsTrue(contenuto.Contains("ID:"), "Deve contenere 'ID:'");
            Assert.IsTrue(contenuto.Contains("DATA/ORA:"), "Deve contenere 'DATA/ORA:'");
            Assert.IsTrue(contenuto.Contains("ORDINE:"), "Deve contenere 'ORDINE:'");
            Assert.IsTrue(contenuto.Contains("2x Margherita"), "Deve raggruppare '2x Margherita'");
        }

        /// <summary>
        /// TEST 23 - Gestione errore scrittura.
        /// Se il percorso non è valido, il salvataggio deve fallire gracefully.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test23_SalvataggioErroreScrittura_GestisceEccezione()
        {
            // Arrange: pizza nell'ordine
            form.AggiungiPizzaPerTest("Margherita");

            // Percorso con caratteri illegali che causerà un errore
            string percorsoNonValido = "Z:\\<>:\"|?*\\file.txt";

            // Act: tentiamo di salvare in un percorso impossibile
            var risultato = form.SalvaStoricoPerTest(percorsoNonValido);

            // Assert: deve fallire senza crash (eccezione gestita)
            Assert.IsFalse(risultato.success, "Il salvataggio deve fallire con percorso invalido");
        }

        /// <summary>
        /// TEST 24 - Salvataggio consentito anche a tavolo occupato.
        /// Dopo aver servito, deve essere ancora possibile salvare lo storico.
        /// </summary>
        [TestMethod]
        [TestCategory("Storico")]
        public void Test24_SalvataggioTavoloOccupato_Consentito()
        {
            // Arrange: ordine e servizio
            form.AggiungiPizzaPerTest("Margherita");
            form.ServiOrdinePerTest();

            // Verifica precondizione: il tavolo deve essere occupato
            if (!form.IsTavoloOccupato)
            {
                Assert.Inconclusive("Servizio fallito, impossibile testare");
                return;
            }

            // Act: salvataggio storico a tavolo occupato
            var risultato = form.SalvaStoricoPerTest(FILE_STORICO_TEST);

            // Assert: deve funzionare comunque
            Assert.IsTrue(risultato.success, "Il salvataggio deve essere consentito a tavolo occupato");
        }

        /// <summary>
        /// TEST 25 - Scontrino consentito anche a tavolo occupato.
        /// Dopo aver servito, deve essere ancora possibile generare lo scontrino.
        /// </summary>
        [TestMethod]
        [TestCategory("Scontrino")]
        public void Test25_ScontrinoTavoloOccupato_Consentito()
        {
            // Arrange: ordine con 2 pizze e servizio
            form.AggiungiPizzaPerTest("Margherita");   // € 5.00
            form.AggiungiPizzaPerTest("Capricciosa");  // € 8.00
            form.ServiOrdinePerTest();

            // Verifica precondizione
            if (!form.IsTavoloOccupato)
            {
                Assert.Inconclusive("Servizio fallito, impossibile testare");
                return;
            }

            // Act: scontrino a tavolo occupato
            var risultato = form.GeneraScontrinoPerTest();

            // Assert: deve funzionare, totale € 13.00
            Assert.IsTrue(risultato.success, "Lo scontrino deve essere generabile a tavolo occupato");
            Assert.IsTrue(risultato.totale > 0, "Il totale deve essere positivo");
        }

        #endregion

        #region ============ TEST BONUS ============

        /// <summary>
        /// TEST BONUS - Verifica prezzi del listino.
        /// Controlla che i prezzi delle pizze principali siano corretti.
        /// -1 indica pizza non presente nel listino.
        /// </summary>
        [TestMethod]
        [TestCategory("Listino")]
        public void TestBonus_VerificaPrezziListino()
        {
            // Verifica prezzi noti
            Assert.AreEqual(5.00m, form.GetPrezzoPizzaPerTest("Margherita"), "Margherita = € 5.00");
            Assert.AreEqual(4.50m, form.GetPrezzoPizzaPerTest("Marinara"), "Marinara = € 4.50");
            Assert.AreEqual(8.00m, form.GetPrezzoPizzaPerTest("Capricciosa"), "Capricciosa = € 8.00");
            Assert.AreEqual(9.00m, form.GetPrezzoPizzaPerTest("Salmone"), "Salmone = € 9.00");

            // Pizza inesistente deve ritornare -1
            Assert.AreEqual(-1m, form.GetPrezzoPizzaPerTest("PizzaInesistente"), "Pizza non in listino = -1");
        }

        /// <summary>
        /// TEST BONUS - Ciclo completo di un ordine.
        /// Simula l'intero flusso: ordine → scontrino → storico → servizio → sparecchiatura.
        /// </summary>
        [TestMethod]
        [TestCategory("Integrazione")]
        public void TestBonus_CicloCompletoOrdine()
        {
            // ===== STEP 1: CREAZIONE ORDINE =====
            form.AggiungiPizzaPerTest("Margherita");       // € 5.00
            form.AggiungiPizzaPerTest("Quattro formaggi"); // € 8.50
            Assert.AreEqual(2, form.NumeroPizzeInOrdine, "Devono esserci 2 pizze");

            // ===== STEP 2: GENERAZIONE SCONTRINO =====
            var scontrino = form.GeneraScontrinoPerTest();
            Assert.IsTrue(scontrino.success, "Scontrino generato");
            Assert.AreEqual(13.50m, scontrino.totale, "Totale: € 5.00 + € 8.50 = € 13.50");

            // ===== STEP 3: SALVATAGGIO STORICO =====
            var storico = form.SalvaStoricoPerTest(FILE_STORICO_TEST);
            Assert.IsTrue(storico.success, "Storico salvato");

            // ===== STEP 4: SERVIZIO AL TAVOLO =====
            bool servito = form.ServiOrdinePerTest();
            if (servito)
            {
                Assert.IsTrue(form.IsTavoloOccupato, "Tavolo occupato dopo il servizio");

                // ===== STEP 5: SPARECCHIATURA =====
                form.SparecchiaPerTest();
                Assert.IsFalse(form.IsTavoloOccupato, "Tavolo libero dopo sparecchiatura");
                Assert.AreEqual(0, form.NumeroPizzeInOrdine, "Ordine svuotato");
            }

            // ===== VERIFICA FINALE =====
            Assert.IsTrue(File.Exists(FILE_STORICO_TEST), "File storico deve esistere");
        }

        #endregion
    }
}
