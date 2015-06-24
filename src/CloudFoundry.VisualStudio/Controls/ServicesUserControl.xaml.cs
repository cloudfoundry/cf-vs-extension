﻿using CloudFoundry.VisualStudio.Forms;
using CloudFoundry.VisualStudio.ProjectPush;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace CloudFoundry.VisualStudio.Controls
{
    /// <summary>
    /// Interaction logic for ServicesUserControl.xaml
    /// </summary>
    public partial class ServicesUserControl : UserControl
    {
        public ServicesUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            var dataContext = (this.DataContext as PublishProfileEditorResources);

            if (dataContext == null)
            {
                throw new InvalidOperationException("DataContext is not a valid PublishProfileEditorResources");
            }

           var spaceInfo=dataContext.Spaces.Where(o=>o.Name == dataContext.PublishProfile.Space).FirstOrDefault();

           if (spaceInfo == null)
           {
               throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not find space {0} in datacontext's spaces", dataContext.PublishProfile.Space));
           }

           CreateServiceForm serviceDialog = new CreateServiceForm(dataContext.Client, spaceInfo.EntityMetadata.Guid.ToGuid());

           serviceDialog.ShowDialog();

           if (serviceDialog.DialogResult == true)
           {
               dataContext.Refresh(PublishProfileRefreshTarget.ServiceInstances);
           }
        }
    }
}
