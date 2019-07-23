using System;
using System.Collections.Generic;
using System.Linq;
using Budgetor.Constants;
using Budgetor.Helpers;
using Budgetor.Models.Contracts;

namespace Budgetor.Models
{
    public class TransactionModalVM : BindableBase
    {
        #region Constructors

        public TransactionModalVM(ITransactionDetail transaction,
                                  List<IComboBoxItem> transactionTypesList,
                                  List<IComboBoxItem> hoursList,
                                  List<IComboBoxItem> meridianList,
                                  List<IComboBoxItem> timeZoneList,
                                  List<IComboBoxItem> categoryList,
                                  List<AccountComboBoxItem> accounts,
                                  bool isEditMode
        )
        {
            OGTransaction = transaction;
            MapOGToNewTransaction();

            TransactionTypesList = new List<IComboBoxItem>();
            TransactionTypesList.AddRange(transactionTypesList);
            HoursList = new List<IComboBoxItem>();
            HoursList.AddRange(hoursList);
            MeridianList = new List<IComboBoxItem>();
            MeridianList.AddRange(meridianList);
            TimeZoneList = new List<IComboBoxItem>();
            TimeZoneList.AddRange(timeZoneList);
            CategoryList = new List<IComboBoxItem>();
            CategoryList.AddRange(categoryList);
            Accounts = new List<AccountComboBoxItem>();
            Accounts.AddRange(accounts);
            IsEditMode = isEditMode;

            ConfigureVMAsPerTransactionType();
        }

        #endregion Constructors

        #region Properties and Fields

        public readonly ITransactionDetail OGTransaction;
        private ITransactionDetail _Transaction;
        public ITransactionDetail Transaction
        {
            get => _Transaction;
            set
            {
                if (value != OGTransaction)
                {
                    IsDirty = true;
                }
                if (value != _Transaction)
                {
                    _Transaction = value;
                    RaisePropertyChanged("Transaction");
                }
            }
        }

        public List<IComboBoxItem> TransactionTypesList { get; set; }
        public int SelectedTransactionType
        {
            get
            {
                return (int)Transaction?.TransactionType;
            }
            set
            {
                var tv = (TransactionType)value;
                if (Transaction?.TransactionType != tv)
                {
                    RaisePropertyChanged("SelectedTransactionType");
                    Transaction.TransactionType = tv;
                }
            }
        }

        public string Display_Amount
        {
            get
            {
                return Transaction.Amount.GetDisplayAmountText();
            }
            set
            {
                decimal.TryParse(value, out decimal result);
                Transaction.Amount = result;
            }
        }

            #region Time

        public bool UseTime { get; set; }

        public List<IComboBoxItem> HoursList { get; set; }
        public int SelectedHour
        {
            get
            {
                var hour = Transaction.DateTime_Occurred.Hour;
                return hour > 12 ? hour - 12 : hour;
            }
            set
            {
                var hour = value;
                if (_SelectedMeridian == "PM")
                {
                    hour += 12;
                }
                Transaction.DateTime_Occurred = new DateTime(Transaction.DateTime_Occurred.Year,
                                                            Transaction.DateTime_Occurred.Month,
                                                            Transaction.DateTime_Occurred.Day,
                                                            hour,
                                                            Transaction.DateTime_Occurred.Minute,
                                                            Transaction.DateTime_Occurred.Second);
                RaisePropertyChanged("SelectedHour");
            }
        }

        public int SelectedMinutes { get; set; }
        public string SelectedMinutesString
        {
            get
            {
                return Transaction.DateTime_Occurred.Minute.ToMinuteString();
            }
            set
            {
                int.TryParse(value, out int newVal);

                Transaction.DateTime_Occurred = new DateTime(Transaction.DateTime_Occurred.Year,
                                                            Transaction.DateTime_Occurred.Month,
                                                            Transaction.DateTime_Occurred.Day,
                                                            Transaction.DateTime_Occurred.Hour,
                                                            newVal,
                                                            Transaction.DateTime_Occurred.Second);
                RaisePropertyChanged("SelectedMinutesString");
            }
        }

        public List<IComboBoxItem> MeridianList { get; set; }
        private string _SelectedMeridian;
        public string SelectedMeridian
        {
            get
            {
                if (SelectedHour > 12)
                {
                    _SelectedMeridian = "PM";
                }
                else
                {
                    _SelectedMeridian = "AM";
                }
                return _SelectedMeridian;
            }
            set
            {
                if (value != _SelectedMeridian)
                {
                    var hour = Transaction.DateTime_Occurred.Hour;
                    if (value == "PM" && hour < 12)
                    {
                        hour += 12;
                    }
                    else if (value == "AM" && hour >= 12)
                    {
                        hour -= 12;
                    }
                    Transaction.DateTime_Occurred = new DateTime(Transaction.DateTime_Occurred.Year,
                                                                Transaction.DateTime_Occurred.Month,
                                                                Transaction.DateTime_Occurred.Day,
                                                                hour,
                                                                Transaction.DateTime_Occurred.Minute,
                                                                Transaction.DateTime_Occurred.Second);

                    _SelectedMeridian = value;
                    RaisePropertyChanged("SelectedMeridian");
                }
            }
        }

        // TODO: Are we going to implement this?
        public List<IComboBoxItem> TimeZoneList { get; set; }
        private int _SelectedTimeZone;
        public int SelectedTimeZone
        {
            get
            {
                return _SelectedTimeZone;
            }
            set
            {
                if (value != _SelectedTimeZone)
                {
                    _SelectedTimeZone = value;
                    RaisePropertyChanged("SelectedTimeZone");
                }
            }
        }

            #endregion Time

            #region Accounts
        public List<AccountComboBoxItem> Accounts { get; set; }

                #region Pre-Configs

        private readonly List<AccountType> DepositFromList = new List<AccountType>()
        {
            AccountType.BankAccount, AccountType.IncomeSource
        };

        private readonly List<AccountType> DepositToList = new List<AccountType>()
        {
            AccountType.BankAccount
        };

        private readonly List<AccountType> PurchaseFromList = new List<AccountType>()
        {
            AccountType.BankAccount
        };

                #endregion Pre-Configs

                #region From
        private bool _ShowFromAccount;
        public bool ShowFromAccount
        {
            get
            {
                return _ShowFromAccount;
            }
            set
            {
                if (value != _ShowFromAccount)
                {
                    _ShowFromAccount = value;
                    RaisePropertyChanged("ShowFromAccount");
                }
            }
        }

        private string _FromAccountLabelCopy;
        public string FromAccountLabelCopy
        {
            get
            {
                return _FromAccountLabelCopy;
            }
            set
            {
                if (value != _FromAccountLabelCopy)
                {
                    _FromAccountLabelCopy = value;
                    RaisePropertyChanged("FromAccountLabelCopy");
                }
            }
        }

        private List<AccountType> _FromAccountTypes;
        private List<AccountType> FromAccountTypes
        {
            get
            {
                if (_FromAccountTypes == null)
                {
                    _FromAccountTypes = new List<AccountType>();
                }
                return _FromAccountTypes;
            }
            set
            {
                _FromAccountTypes = value;
                RaisePropertyChanged("FromAccounts");
            }
        }
        public List<AccountComboBoxItem> FromAccounts
        {
            get
            {
                return Accounts.FindAll(x => FromAccountTypes.Any(y => y == x.AccountType));
            }
        }

        private int? _SelectedFromAccount;
        public int SelectedFromAccount
        {
            get
            {
                if (!_SelectedFromAccount.HasValue)
                {
                    SetSelectedFromAccount(false);
                }
                return _SelectedFromAccount ?? 0;
            }
            set
            {
                if (value != _SelectedFromAccount)
                {
                    _SelectedFromAccount = value;
                    this.Transaction.FromAccount = GetBasicAccountById(value);
                    RaisePropertyChanged("SelectedFromAccount");
                    //SetSelectedFromAccount();
                }
            }
        }
        #endregion From

        #region To
        private bool _ShowToAccount;
        public bool ShowToAccount
        {
            get
            {
                return _ShowToAccount;
            }
            set
            {
                if (value != _ShowToAccount)
                {
                    _ShowToAccount = value;
                    RaisePropertyChanged("ShowToAccount");
                }
            }
        }
        
        private string _ToAccountLabelCopy;
        public string ToAccountLabelCopy
        {
            get
            {
                return _ToAccountLabelCopy;
            }
            set
            {
                if (value != _ToAccountLabelCopy)
                {
                    _ToAccountLabelCopy = value;
                    RaisePropertyChanged("ToAccountLabelCopy");
                }
            }
        }

        private List<AccountType> _ToAccountTypes;
        private List<AccountType> ToAccountTypes
        {
            get
            {
                if (_ToAccountTypes == null)
                {
                    _ToAccountTypes = new List<AccountType>();
                }
                return _ToAccountTypes;
            }
            set
            {
                _ToAccountTypes = value;
                RaisePropertyChanged("ToAccounts");
                SetSelectedToAccount();
            }
        }
        public List<AccountComboBoxItem> ToAccounts
        {
            get
            {
                return Accounts.FindAll(x => ToAccountTypes.Any(y => y == x.AccountType));
            }
        }

        private int? _SelectedToAccount;
        public int SelectedToAccount
        {
            get
            {
                if (!_SelectedToAccount.HasValue)
                {
                    SetSelectedToAccount(false);
                }
                return _SelectedToAccount ?? 0;
            }
            set
            {
                if (value != _SelectedToAccount)
                {
                    _SelectedToAccount = value;
                    this.Transaction.FromAccount = GetBasicAccountById(value);
                    RaisePropertyChanged("SelectedToAccount");
                }
            }
        }
                #endregion To

            #endregion Accounts

            // TODO: need to implement Categories yet
            #region Categories

        public bool ShowCategory { get; set; }

        public List<IComboBoxItem> CategoryList { get; set; }
        public int SelectedCategory { get; set; }

            #endregion Categories

            #region Modal Config

        private bool IsDirty;
        public bool IsEditMode { get; set; }
        public bool IsAddMode
        {
            get { return !IsEditMode; }
            set { IsEditMode = !value; }
        }

        public string ModalTitle
        {
            get
            {
                if (string.IsNullOrEmpty(Transaction?.Title))
                {
                    return "Add a new transaction";
                }
                else
                {
                    return Transaction.Title;
                }
            }
        }

            #endregion Modal Config

        #endregion Properties

        #region Exposed Methods

        internal void ConfigureVMAsPerTransactionType()
        {
            switch (Transaction.TransactionType)
            {
                case TransactionType.Deposit:
                    FromAccountLabelCopy = "Deposit from where?";
                    ToAccountLabelCopy = "Deposit this into which account?";
                    FromAccountTypes = DepositFromList;
                    ToAccountTypes = DepositToList;
                    ShowToAccount = true;
                    ShowFromAccount = true;
                    break;
                case TransactionType.Purchase:
                    ShowToAccount = false;
                    FromAccountLabelCopy = "Which account did you use to make the purchase?";
                    FromAccountTypes = PurchaseFromList;
                    ToAccountLabelCopy = "";
                    ShowFromAccount = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        internal void ResetVM()
        {
            MapOGToNewTransaction();
            SetSelectedToAccount();
            SetSelectedFromAccount();
        }

        internal TransactionSaveInfo ExportSaveModel() => new TransactionSaveInfo(this);

        #endregion Exposed Methods

        #region Private Methods

        private void SetSelectedToAccount(bool update = true)
        {
            _SelectedToAccount = Transaction?.ToAccount?.AccountId ?? ToAccounts.GetDefaultAccount();
            if (update)
                RaisePropertyChanged("SelectedToAccount");
        }

        private void SetSelectedFromAccount(bool update = true)
        {
            _SelectedFromAccount = Transaction?.FromAccount?.AccountId ?? FromAccounts.GetDefaultAccount();
            if (update)
                RaisePropertyChanged("SelectedFromAccount");
        }

        private void MapOGToNewTransaction()
        {
            Transaction = new TransactionDetailBase(OGTransaction.TransactionType)
            {
                Amount = OGTransaction.Amount,
                DateTime_Created = OGTransaction.DateTime_Created,
                DateTime_Occurred = OGTransaction.DateTime_Occurred,
                FromAccount = OGTransaction.FromAccount,
                IsConfirmed = OGTransaction.IsConfirmed,
                IsUserCreated = OGTransaction.IsUserCreated,
                OccerrenceAccount = OGTransaction.OccerrenceAccount,
                Title = OGTransaction.Title,
                ToAccount = OGTransaction.ToAccount
            };
        }

        private AccountBasicInfo GetBasicAccountById(int accountId)
        {
            var cbItem = Accounts.FirstOrDefault(x => x.AccountId == accountId);
            return new AccountBasicInfo(cbItem.AccountType)
            {
                AccountId = cbItem.AccountId,
                AccountName = cbItem.AccountName
            };
        }

        #endregion Private Methods
    }
}
