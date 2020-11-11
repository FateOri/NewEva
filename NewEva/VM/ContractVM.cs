﻿using NewEva.DbLayer;
using NewEva.Model;
using NewEva.VM.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;

namespace NewEva.VM
{
    public class ContractVM : PageVM
    {
        private PageVM currentPage;
        public PageVM CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage?.Write();
                SetProperty(ref currentPage, value);
            }
        }
        public IEnumerable<string> TypeCosts { get; }
        public bool IsEdit { get; }
        public ContractVM(Contracts contract = null)
        {
            if (IsEdit = contract != null)
            {
                Id = contract.Id;
                Number = contract.Number;
                DateContract = contract.DateContract;
                IsTypeCost = contract.TypeCost;
                Target = contract.Target;
            }
            pages = new string[]
            {
                "PrivatePersonListVM",
                "OrganizationListVM"
            };
            TypeCosts = LocalStorage.TypeCosts;
            NewCustomerPage = new RelayCommand(_ => NewCustomerCommand());
        }

        private string[] pages;
        public string[] Pages
        {
            get => pages;
            set
            {
                SetProperty(ref pages, value);
            }
        }
        private int currentIndex;
        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                CurrentPage = CreatePageByName(pages[value]);
                SetProperty(ref currentIndex, value);
            }
        }
        public PageVM CreatePageByName(string pageName)
        {
            if (pageName == "PrivatePersonListVM")
                return new PrivatePersonListVM();
            else if (pageName == "OrganizationListVM")
                return new OrganizationListVM();
            else
                return null;
        }

        public ICommand NewCustomerPage { get; }
        public void NewCustomerCommand()
        {
            if (CurrentPage is PrivatePersonListVM)
                CurrentPage = new PrivatePersonVM();
            else if (CurrentPage is OrganizationListVM)
                CurrentPage = new OrganizationVM();
        }

        #region Property
        public int Id { get; set; }
        private string number;
        [Required(ErrorMessage = "Не указан номер договора")]
        [StringLength(20)]
        public string Number 
        {
            get => number;
            set
            {
                ValidateProperty(value);
                SetProperty(ref number, value);
            }
        }
        private DateTime? dateContract;
        [Required(ErrorMessage = "Не указана дата договора")]
        public DateTime? DateContract
        {
            get => dateContract;
            set
            {
                ValidateProperty(value);
                SetProperty(ref dateContract, value);
            }
        }
        private string isTypeCost;
        [Required(ErrorMessage = "Не выбран тип стоимости")]
        public string IsTypeCost
        {
            get => isTypeCost;
            set
            {
                ValidateProperty(value);
                SetProperty(ref isTypeCost, value);
            }
        }
        private string target;
        [Required(ErrorMessage = "Не указан цель оценки")]
        [StringLength(250)]
        public string Target
        {
            get => target;
            set
            {
                ValidateProperty(value);
                SetProperty(ref target, value);
            }
        }
        #endregion

        public Contracts ToContracts()
        {
            var contract = new Contracts
            {
                Id = Id,
                Number = Number,
                DateContract = DateContract,
                TypeCost = IsTypeCost,
                Target = Target
            };
            return contract;
        }
        public int AddContract()
        {
            try
            {
                var contract = ToContracts();
                DataBase.Write(contract);
                var newId = contract.Id;
                return newId;
            }
            catch
            {

                return -1;
            }
        }
        public bool UpdateContract()
        {
            try
            {
                var contract = ToContracts();
                DataBase.UpdateData(contract);
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
