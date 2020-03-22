﻿// Copyright (c) Quarrel. All rights reserved.

using GalaSoft.MvvmLight.Ioc;
using Quarrel.Navigation;
using Quarrel.Services.Cache;
using Quarrel.Services.Clipboard;
using Quarrel.Services.DispatcherHelperEx;
using Quarrel.Services.Resources;
using Quarrel.Services.Settings;
using Quarrel.Services.Voice.Audio.In;
using Quarrel.Services.Voice.Audio.Out;
using Quarrel.SubPages;
using Quarrel.SubPages.GuildSettings;
using Quarrel.SubPages.UserSettings;
using Quarrel.ViewModels.Services.Cache;
using Quarrel.ViewModels.Services.Clipboard;
using Quarrel.ViewModels.Services.Discord.Channels;
using Quarrel.ViewModels.Services.Discord.CurrentUser;
using Quarrel.ViewModels.Services.Discord.Friends;
using Quarrel.ViewModels.Services.Discord.Guilds;
using Quarrel.ViewModels.Services.Discord.Presence;
using Quarrel.ViewModels.Services.Discord.Rest;
using Quarrel.ViewModels.Services.DispatcherHelper;
using Quarrel.ViewModels.Services.Gateway;
using Quarrel.ViewModels.Services.Navigation;
using Quarrel.ViewModels.Services.Resources;
using Quarrel.ViewModels.Services.Settings;
using Quarrel.ViewModels.Services.Voice;
using Quarrel.ViewModels.Services.Voice.Audio.In;
using Quarrel.ViewModels.Services.Voice.Audio.Out;
using System;
using Windows.ApplicationModel.Store;

namespace Quarrel.ViewModels
{
    /// <summary>
    /// Initializes the ViewModel and Services.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class and the <see cref="MainViewModel"/>.
        /// Creates and registers all the services with <see cref="SimpleIoc.Default"/>.
        /// </summary>
        public ViewModelLocator()
        {
            var navigationService = new SubFrameNavigationService();
            navigationService.Configure("AboutPage", typeof(AboutPage));
            navigationService.Configure("AddChannelPage", typeof(AddChannelPage));
            navigationService.Configure("AttachmentPage", typeof(AttachmentPage));
            navigationService.Configure("CreditPage", typeof(CreditPage));
            navigationService.Configure("DiscordStatusPage", typeof(DiscordStatusPage));
            navigationService.Configure("GuildSettingsPage", typeof(GuildSettingsPage));
            navigationService.Configure("LicensesPage", typeof(LicensesPage));
            navigationService.Configure("LoginPage", typeof(LoginPage));
            navigationService.Configure("TopicPage", typeof(TopicPage));
            navigationService.Configure("UserProfilePage", typeof(UserProfilePage));
            navigationService.Configure("UserSettingsPage", typeof(UserSettingsPage));
            navigationService.Configure("WhatsNewPage", typeof(WhatsNewPage));

            SimpleIoc.Default.Register<IDispatcherHelper, DispatcherHelperEx>();
            SimpleIoc.Default.Register<ISubFrameNavigationService>(() => navigationService);

            SimpleIoc.Default.Register<ICacheService, CacheService>();
            SimpleIoc.Default.Register<IClipboardService, ClipboardService>();
            SimpleIoc.Default.Register<IResourceService, ResourceService>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<IServiceProvider>(() => App.ServiceProvider);
            SimpleIoc.Default.Register<IGatewayService, GatewayService>();
            SimpleIoc.Default.Register<IDiscordService, DiscordService>();
            SimpleIoc.Default.Register<IPresenceService, PresenceService>();
            SimpleIoc.Default.Register<IFriendsService, FriendsService>();
            SimpleIoc.Default.Register<IChannelsService, ChannelsService>();
            SimpleIoc.Default.Register<IGuildsService, GuildsService>();
            SimpleIoc.Default.Register<IAudioInService, AudioInService>();
            SimpleIoc.Default.Register<IAudioOutService, AudioOutService>();
            SimpleIoc.Default.Register<ICurrentUserService, CurrentUsersService>();
            SimpleIoc.Default.Register<IVoiceService, VoiceService>();

            LicenseInformation licenseInformation = CurrentApp.LicenseInformation;
            if (licenseInformation.ProductLicenses["RemoveAds"].IsActive ||
                licenseInformation.ProductLicenses["Remove Ads"].IsActive ||
                licenseInformation.ProductLicenses["Polite Dontation"].IsActive ||
                licenseInformation.ProductLicenses["SignificantDontation"].IsActive ||
                licenseInformation.ProductLicenses["OMGTHXDonation"].IsActive ||
                licenseInformation.ProductLicenses["RidiculousDonation"].IsActive)
            {
                SimpleIoc.Default.GetInstance<ISettingsService>().Roaming.SetValue(SettingKeys.AdsRemoved, true);
            }
            else
            {
                // If none are active, set to false if not already set
                SimpleIoc.Default.GetInstance<ISettingsService>().Roaming.SetValue(SettingKeys.AdsRemoved, false, false);
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Gets the <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
