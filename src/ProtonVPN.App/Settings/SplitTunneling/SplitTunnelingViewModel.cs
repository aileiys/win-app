﻿/*
 * Copyright (c) 2020 Proton Technologies AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ProtonVPN is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ProtonVPN.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.ComponentModel;
using ProtonVPN.Common;
using ProtonVPN.Core.MVVM;
using ProtonVPN.Core.Settings;

namespace ProtonVPN.Settings.SplitTunneling
{
    public class SplitTunnelingViewModel: ViewModel, ISettingsAware
    {
        private readonly IAppSettings _appSettings;

        public SplitTunnelingViewModel(IAppSettings appSettings, AppListViewModel apps, SplitTunnelingIpListViewModel ips)
        {
            _appSettings = appSettings;
            Apps = apps;
            Ips = ips;
        }

        public bool Enabled
        {
            get => _appSettings.SplitTunnelingEnabled;
            set
            {
                _appSettings.SplitTunnelingEnabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }

        public SplitTunnelMode SplitTunnelMode
        {
            get => _appSettings.SplitTunnelMode;
            set
            {
                _appSettings.SplitTunnelMode = value;
                OnPropertyChanged(nameof(SplitTunnelMode));
            }
        }

        public AppListViewModel Apps { get; }

        public IpListViewModel Ips { get; }

        private bool _disconnected;
        public bool Disconnected
        {
            get => _disconnected;
            set => Set(ref _disconnected, value);
        }

        public void OnActivate()
        {
            Apps.OnActivate();
            Ips.OnActivate();
        }

        public void OnAppSettingsChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IAppSettings.KillSwitch)) && _appSettings.KillSwitch)
            {
                _appSettings.SplitTunnelingEnabled = false;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }
}
