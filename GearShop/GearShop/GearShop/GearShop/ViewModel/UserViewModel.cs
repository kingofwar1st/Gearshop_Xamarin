using GearShop.ClassGear;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GearShop.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private UserInformation _user;

        public UserInformation UserInformation
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(UserInformation));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
