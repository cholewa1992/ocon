﻿using System;
using System.Collections.Generic;
using System.Net;
using ContextawareFramework.NetworkHelper;

namespace ContextawareFramework
{
    public class ContextCentral
    {

        private readonly ICommunicationHelper _comHelper;
        private readonly ContextFilter _contextFilter;
        private Dictionary<Guid,IPEndPoint> _clients = new Dictionary<Guid, IPEndPoint>(); 


        public ContextCentral(ContextFilter contextFilter, ICommunicationHelper communicationHelper)
        {
            _contextFilter = contextFilter;
            _comHelper = communicationHelper;
        }

        public void Initialize()
        {
            // Set up
            _contextFilter.SituationStateChanged +=
                (sender, args) =>
                    _comHelper.SendSituationState(args.Situation, _clients[args.Situation.SubscribersAddresse]);

            // Set up events
            _comHelper.IncommingClient          += (sender, args) =>
            {
                if (!_clients.ContainsKey(args.Guid)) _clients.Add(args.Guid, args.Ipep);
            };
            _comHelper.IncommingEntityEvent     += (sender, args) => _contextFilter.TrackEntity(args.Entity);
            _comHelper.IncommingSituationSubscribtionEvent  += (sender, args) => //TODO;

            // Start listening for widgets and clients
            _comHelper.StartListen();

            // Multicast presence to widgets
            _comHelper.Broadcast();
        }
    }
}