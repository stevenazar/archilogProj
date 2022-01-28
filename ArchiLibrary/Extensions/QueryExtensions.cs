using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using ArchiLibrary.Models;
using System.Linq;
using System.Linq.Expressions;

namespace ArchiLibrary.Extensions
{
    public static class QueryExtensions
    {
        // méthode static qui permet d'obtenir les propriétés d'un objet et par la suite effetcuer un tri
        // en fonction de la valeur d'un champ de la base
        public static IOrderedQueryable<TModel> Sort<TModel>(this IQueryable<TModel> query, Params param)
            {
                if (param.HasAscOrder())
                {
                    string champ = param.Asc;
                    //var property = typeof(TModel).GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    // return query.OrderBy(x => typeof(TModel).GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public));

                    //créer lambda
                    var parameter = Expression.Parameter(typeof(TModel), "x");
                    var property = Expression.Property(parameter, champ);
                    var o = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter);

                    //utilise
                    return query.OrderBy(lambda);

                }
                else if(param.HasDescOrder())
                {
                    string champ = param.Desc;
                    //var property = typeof(TModel).GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    // return query.OrderBy(x => typeof(TModel).GetProperty(champ, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public));

                    //créer lambda
                    var parameter = Expression.Parameter(typeof(TModel), "x");
                    var property = Expression.Property(parameter, champ);
                    var o = Expression.Convert(property, typeof(object));
                    var lambda = Expression.Lambda<Func<TModel, object>>(o, parameter); 

                    //utilise
                    return query.OrderByDescending(lambda);
                }else
                    return (IOrderedQueryable<TModel>)query;

        }

        

        
    }
}
