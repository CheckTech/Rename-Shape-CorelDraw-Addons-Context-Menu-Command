using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
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
using RenameMe.DataSource;


namespace RenameMe
{
    public partial class DockerUI : Window
    {
       
        private Styles.StylesController stylesController;
        public string ShapeName { get; set; }
        public bool Renamed { get; set; }
        public DockerUI()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = RenameWarpercs.P.X;
            this.Top = RenameWarpercs.P.Y;
            Renamed = false;
            textBlocShapeName.Text = RenameWarpercs.corelApp.ActiveShape.Name;
            try
            {
               
                stylesController = new Styles.StylesController(this.Resources, RenameWarpercs.corelApp);
            
            }
            catch
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            stylesController.LoadThemeFromPreference();
            textBlocShapeName.Focus();
            
        }
        protected override void OnDeactivated(EventArgs e)
        {
            //base.OnDeactivated(e);
            try
            {
                this.Close();
            }
            catch { }
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            // base.OnLostFocus(e);
            this.Close();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            //base.OnKeyUp(e);
            if (e.Key == Key.Escape)
                this.Close();
            if(e.Key == Key.Enter)
            {
                Renamed = true;
                ShapeName = textBlocShapeName.Text;
                this.Close();
            }
        }
    }
}
