namespace ShowMeNow.API.Helpers
{
    using System;

    using AutoMapper;

    public class GuidToStringConverter : ITypeConverter<Guid?, int?>, ITypeConverter<byte?, int?>
    {
        public int? Convert(ResolutionContext context)
        {
            return context.IsSourceValueNull ? (int?)null : System.Convert.ToInt32(context.SourceValue);
        }
    }
}