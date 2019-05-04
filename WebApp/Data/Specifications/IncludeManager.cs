﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using WebApp.Data.Entities;

namespace WebApp.Data.Specifications
{
    public class IncludeManager
    {
        private List<IncludeChain> includeChains;

        public IncludeManager()
        {
            includeChains = new List<IncludeChain>();
        }

        public void AddIncludeChain(IncludeChain includeChain)
        {
            includeChains.Add(includeChain);
        }

        public virtual IQueryable<T> ApplyIncludeChains<T>(IQueryable<T> query) where T:class
        {
            var ret = query;

            foreach (var chain in includeChains)
            {
                
                var param = chain.Include.Parameters[0];

                if (param == null)
                    throw new InvalidOperationException(
                        $"Given lambda expression has no parameters, required one. Expression :{chain.Include.ToString()}");
                if(param.Type != typeof(T))
                    throw new InvalidOperationException(
                        "Type of lambda expression parameter is diffrent from IQuerable generic type."+
                        $"Parameter type: {param.Type}, IQuerable type: {typeof(T).ToString()}");

                var member = (chain.Include.Body as MemberExpression).Member;
                if(member == null || member.MemberType != MemberTypes.Property)
                    throw new InvalidOperationException(
                        $"Given lambda expression is not property access expression. Expression :{chain.Include.ToString()}");

                var memberType = ((PropertyInfo)member).PropertyType;
                var enumMemberTypes = memberType.IsGenericType ? memberType.GetGenericArguments() : null;

                if(enumMemberTypes.Count() != 1)
                    throw new InvalidOperationException(
                         $"Member generic type has {enumMemberTypes.Count()} gneric arguments. Should be 1");

                if(chain.ThenIncludes.Count == 0)
                {
                    var convInclude = chain.Include as Expression<Func<T, object>>;
                    
                    return ret.Include(convInclude);
                }

                foreach (var thenInclude in chain.ThenIncludes)
                {
                    var tParam = thenInclude.Parameters[0];
                    if (tParam == null)
                        throw new InvalidOperationException(
                            $"Given lambda expression has no parameters, required one. Expression :{thenInclude.ToString()}");
                    if (enumMemberTypes != null)
                    {
                        if (tParam.Type != enumMemberTypes[0])
                            throw new InvalidOperationException(
                                "Generic type of IEnumerable member of include is diffrent from ThenInclude expression parameter type." +
                                $" Include type: {enumMemberTypes[0]}, ThenInclude type: {tParam.Type}");
                    }
                    else if (tParam.Type != memberType)
                        throw new InvalidOperationException(
                            "Type of include expression parameter is diffrent from ThenInclude expression parameter type." +
                            $" Include type: {member.DeclaringType}, ThenInclude type: {tParam.Type}");

                    var tMember = (thenInclude.Body as MemberExpression).Member;
                    if (tMember == null)
                        throw new InvalidOperationException(
                            $"Given lambda expression is not member expression. Expression :{chain.Include.ToString()}");

                    if (tParam.Type == typeof(TripUser))
                    {
                        if (enumMemberTypes != null)
                        {
                            var convInclude = chain.Include as Expression<Func<T,IEnumerable <TripUser>>>;
                            var convTInclude = thenInclude as Expression<Func<TripUser, object>>;

                            ret = ret.Include(convInclude).ThenInclude(convTInclude);
                        }
                        else
                        {
                            var convInclude = chain.Include as Expression<Func<T, TripUser>>;
                            var convTInclude = thenInclude as Expression<Func<TripUser, object>>;

                            ret = ret.Include(convInclude).ThenInclude(convTInclude);
                        }
                    }
                }
            }

            return ret;
        }

    }
}
