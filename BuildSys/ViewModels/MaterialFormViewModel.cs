using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    class MaterialFormViewModel : BaseViewModel
    {

        MainViewModel parent;

        public MaterialFormViewModel(MainViewModel parent)
        {
            this.parent = parent;
            material = new MaterialModel();
            btnText = "Register Material";
        }

        public MaterialFormViewModel(MainViewModel parent, int materalId)
        {
            this.parent = parent;

            material = MaterialModel.getMaterial(materalId);
            btnText = "Update Material";
        }

        public MaterialModel material { get; set; }
        public String btnText { get; set; }

        public ICommand saveMaterialCommand
        {
            get
            {
                return new RelayCommand(param => saveMaterial(), param => canSaveMaterial());
            }
        }

        public void saveMaterial()
        {
            material.validateAllProps();

            if (!material.HasErrors)
            {
                if (btnText.Equals("Update Material"))
                {
                    // Update the Customer's record
                    material.update();

                    MessageBox.Show("Material Updated Successfully");

                    parent.ViewModel = new MaterialManageViewModel(parent);
                }
                else
                {
                    material.insertMaterial();

                    MessageBox.Show("Sucessfully Register a new Material");

                    // Reset the form
                    parent.ViewModel = new MaterialFormViewModel(parent);
                }
            }
        }

        public Boolean canSaveMaterial()
        {
            return !material.HasErrors;
        }

    }

}
