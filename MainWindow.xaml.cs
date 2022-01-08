using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoLotModel;
using System.Data.Entity;
using System.Data;

namespace ATM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource cardVSource;
        CollectionViewSource tranzactiiVSource;
        CollectionViewSource contVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource cardViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cardViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // cardViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource tranzactiiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tranzactiiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // tranzactiiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource contViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("contViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // contViewSource.Source = [generic data source]

            cardVSource =((System.Windows.Data.CollectionViewSource)(this.FindResource("cardViewSource")));
            cardVSource.Source = ctx.Cards.Local;
            ctx.Cards.Load();

           tranzactiiVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tranzactiiViewSource")));
           tranzactiiVSource.Source = ctx.Tranzactiis.Local;
            ctx.Tranzactiis.Load();
            
            contVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("contViewSource")));
            contVSource.Source = ctx.Conts.Local;
            ctx.Conts.Load();


        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlAutoLot.SelectedItem as TabItem;
            switch (ti.Header)
            {
                case "Card":
                    SaveCards();
                    break;
                case "Tranzactii":
                    SaveTranzactiis();
                    break;
                case "Cont":
                    SaveConts();
                    break;
            }
            ReInitialize();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void SaveCards()
        {
            Card card = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Card entity
                    card = new Card()
                    {
                        PIN = Int32.Parse(pINTextBox.Text.Trim()),
                        TipCardID = Int32.Parse(tipCardIDTextBox.Text.Trim()),
                        ContID = Int32.Parse(contIDTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Cards.Add(card);
                    cardVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    card = (Card)cardDataGrid.SelectedItem;
                    card.PIN = Int32.Parse(pINTextBox.Text.Trim());
                    card.TipCardID = Int32.Parse(tipCardIDTextBox.Text.Trim());
                    card.ContID = Int32.Parse(contIDTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    card = (Card)cardDataGrid.SelectedItem;
                    ctx.Cards.Remove(card);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cardVSource.View.Refresh();
            }

        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            cardVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            cardVSource.View.MoveCurrentToNext();
        }

        private void SaveTranzactiis()
        {
            Tranzactii tranzactii = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Tranzactii entity
                    tranzactii = new Tranzactii()
                    {
                        ContID = Int32.Parse(contIDTextBox1.Text.Trim()),
                        data_tranzactie = data_tranzactieDatePicker.DisplayDate,
                        suma = Int32.Parse(sumaTextBox.Text.Trim()),
                        TipTranzactieID = Int32.Parse(tipTranzactieIDTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Tranzactiis.Add(tranzactii);
                    tranzactiiVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    tranzactii = (Tranzactii)tranzactiiDataGrid.SelectedItem;
                    tranzactii.ContID = Int32.Parse(contIDTextBox1.Text.Trim());
                    tranzactii.data_tranzactie = data_tranzactieDatePicker.DisplayDate;
                    tranzactii.suma = Int32.Parse(sumaTextBox.Text.Trim());
                    tranzactii.TipTranzactieID = Int32.Parse(tipTranzactieIDTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    tranzactii = (Tranzactii)tranzactiiDataGrid.SelectedItem;
                    ctx.Tranzactiis.Remove(tranzactii);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                tranzactiiVSource.View.Refresh();
            }

        }

        private void SaveConts()
        {
            Cont cont = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Cont entity
                    cont= new Cont()
                    {
                        numar_cont = numar_contTextBox.Text.Trim(),
                        sold = Int32.Parse(soldTextBox.Text.Trim()),
                        data_deschiderii = data_deschideriiDatePicker.DisplayDate,
                        ClientID = Int32.Parse(clientIDTextBox.Text.Trim()),
                        TipContBancarID = Int32.Parse(tipContBancarIDTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Conts.Add(cont);
                    contVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    cont = (Cont)contDataGrid.SelectedItem;
                    cont.numar_cont = numar_contTextBox.Text.Trim();
                    cont.sold = Int32.Parse(soldTextBox.Text.Trim());
                    cont.data_deschiderii = data_deschideriiDatePicker.DisplayDate;
                    cont.ClientID = Int32.Parse(clientIDTextBox.Text.Trim());
                    cont.TipContBancarID = Int32.Parse(tipContBancarIDTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    cont = (Cont)contDataGrid.SelectedItem;
                    ctx.Conts.Remove(cont);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                contVSource.View.Refresh();
            }

        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {
            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }

        private void btnPrevious1_Click(object sender, RoutedEventArgs e)
        {
            tranzactiiVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            tranzactiiVSource.View.MoveCurrentToNext();
        }

        private void btnPrevious2_Click(object sender, RoutedEventArgs e)
        {
           contVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            contVSource.View.MoveCurrentToNext();
        }

    }
}
