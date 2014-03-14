﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ContextawareFramework
{
    public class Situation: ISituation
    {

        private Guid _id = Guid.NewGuid();
        public Guid Id
        {
            get { return _id; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(value), "Description argument can't be null or empty");
                _description = value;
            }
        }


        private Predicate<ICollection<IEntity>> _situationPredicate;
        public Predicate<ICollection<IEntity>> SituationPredicate
        {
            get { return _situationPredicate; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null, "SituationPredicate argument can't be null");
            }
        }




        

    }
}