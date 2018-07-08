namespace Kritikos.Services.Extensions
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq.Expressions;
	using AutoMapper;

	[SuppressMessage("StyleCop", "SA1618", Justification = "Documented on AutoMapper")]
	[SuppressMessage("StyleCop", "SA1611", Justification = "Documented on AutoMapper")]
	[SuppressMessage("StyleCop", "SA1615", Justification = "Documented on AutoMapper")]
	public partial class Extensions
	{
		/// <summary>
		/// Convenient extension for ignoring properties between automapper source and destination
		/// </summary>
		public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
			this IMappingExpression<TSource, TDestination> map,
			Expression<Func<TDestination, object>> selector)
		{
			map.ForMember(selector, config => config.Ignore());
			return map;
		}

		/// <summary>
		/// Convenient extension for mapping properties between automapper destination and source
		/// </summary>
		public static IMappingExpression<TSource, TDestination> MapProperty<TSource, TDestination, TProperty>(
			this IMappingExpression<TSource, TDestination> map,
			Expression<Func<TSource, TProperty>> sourceMember,
			Expression<Func<TDestination, object>> targetMember)
		{
			map.ForMember(targetMember, opt => opt.MapFrom(sourceMember));
			return map;
		}
	}
}
