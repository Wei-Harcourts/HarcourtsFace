using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Harcourts.Face.WebsiteCommon.ApiServicing
{
    /// <summary>
    /// Represents the object that can be sealed.
    /// </summary>
    public class SealableObject
    {
        private bool _sealed;
        private IDictionary<string, object> _valueBag;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SealableObject()
        {
            _valueBag = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        protected TProperty GetPropertyValue<TProperty>(Expression<Func<TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new NotSupportedException("Property expression must be member expression.");
            }

            var propertyName = memberExpression.Member.Name;
            if (!_valueBag.ContainsKey(propertyName))
            {
                return default(TProperty);
            }
            return (TProperty) _valueBag[propertyName];
        }

        /// <summary>
        /// Sets property value.
        /// </summary>
        protected void SetPropertyValue<TProperty>(Expression<Func<TProperty>> expression, TProperty value)
        {
            if (_sealed)
            {
                throw new InvalidOperationException("Cannot set property once object is sealed.");
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new NotSupportedException("Property expression must be member expression.");
            }

            var propertyName = memberExpression.Member.Name;
            _valueBag[propertyName] = value;
        }

        /// <summary>
        /// Seals the object.
        /// </summary>
        public void Seal()
        {
            if (_sealed)
            {
                return;
            }

            _valueBag = new ReadOnlyDictionary<string, object>(_valueBag);
            _sealed = true;
        }
    }
}
