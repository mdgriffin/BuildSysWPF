using BuildSys.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class MaterialManageViewModel : BaseViewModel
    {

        public MaterialManageViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            // Get the list of all active materials
            materialList = MaterialModel.getMaterialList();
            // Get list of all deleted materials
            deletedMaterialList = MaterialModel.getDeletedMaterialList();

            // Keep a copy of the materialList so that we can restore the list after filtering
            originalMaterialList = new ObservableCollection<MaterialModel>(materialList);
        }

        // Filter used in Material Filter input
        private String _materialFilter = "";
        public String materialFilter
        {
            get
            {
                return _materialFilter;
            }
            set
            {
                if (value != _materialFilter)
                {
                    _materialFilter = value;
                    // Filter the materials when the input changes
                    filterMaterials();
                }
            }
        }

        // Public properties accessed by the view
        public ObservableCollection<MaterialModel> deletedMaterialList { get; set; }

        private ObservableCollection<MaterialModel> originalMaterialList;

        // The Material list bound to the Data Grid in view
        private ObservableCollection<MaterialModel> _materialList = new ObservableCollection<MaterialModel>();
        public ObservableCollection<MaterialModel> materialList
        {
            get { return _materialList ?? (_materialList = new ObservableCollection<MaterialModel>()); }
            set
            {
                if (value == null) return;
                _materialList = value;
                // Notify so that the view can be updated
                NotifyPropertyChanged("materialList");
            }
        }

        // Command to edit the material
        public ICommand editMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => editMaterial((int)materialId), param => true);
            }
        }

        // Command to delete the material
        public ICommand deleteMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => deleteMaterial((int)materialId), param => true);
            }
        }

        // Command to restore the material
        public ICommand restoreMaterialCmd
        {
            get
            {
                return new RelayCommand((materialId) => restoreMaterial((int)materialId), param => true);
            }
        }

        // Edit material command
        public void editMaterial(int materialId)
        {
            navigateTo(new MaterialFormViewModel(parent, materialId));
        }

        // Command to delete the material
        public void deleteMaterial(int materialId)
        {
            // Delete the material from the database
            MaterialModel.deleteMaterial(materialId);
            // Remove the material from the deleted material list
            MaterialModel deletedMaterial = materialList.Where(mat => mat.materialId == materialId).First();

            // Remove from the active material list
            materialList.Remove(deletedMaterial);
            originalMaterialList.Remove(deletedMaterial);

            // Add to the deleted material list
            deletedMaterialList.Add(deletedMaterial);
        }

        // Restores a previously deleted material
        public void restoreMaterial(int materialId)
        {
            // Delete form Db
            MaterialModel.restoreMaterial(materialId);

            // Remove from deleted list and add to active list
            MaterialModel restoredMaterial = deletedMaterialList.Where(mat => mat.materialId == materialId).First();
            deletedMaterialList.Remove(restoredMaterial);

            materialList.Add(restoredMaterial);
            originalMaterialList.Add(restoredMaterial);

            // Refilter the materials
            filterMaterials();
        }

        // Filters the list of materials
        public void filterMaterials()
        {
            materialList = new ObservableCollection<MaterialModel>(originalMaterialList);
            Regex matchName = new Regex(@"^" + materialFilter + @".+", RegexOptions.IgnoreCase);

            if (materialFilter.Length > 0)
            {
                // Filter based on material name
                materialList.Where(mat => !matchName.IsMatch(mat.name))
                    .ToList()
                    .All(i => materialList.Remove(i));
            }
        }

    }

}
