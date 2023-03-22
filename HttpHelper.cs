public static class HttpHelper
{
	/// <summary>
	/// This class is like KeyValuePair, parameter value will be modified based on the assigned value of ToValue
	/// </summary>
	public class UrlParameter
	{
		private string _from;
		private string _to;
		public string FromValue
		{
			get => _from;
			set
			{
				//prevent stupid null values
				_from = value ?? string.Empty;
			}
		}
		public string ToValue
		{
			get => _to;
			set
			{
				//prevent stupid null values
				_to = value ?? string.Empty;
			}
		}
	}

	/// <summary>
	/// Manipulate multiple paramater key by grouping
	/// </summary>
	public class UrlParameterGroup
	{
		private string _parameterName;
		/// <summary>
		/// The Parameter Name - the value of this parameter will be based on the Paramaters
		/// </summary>
		public string ParameterName
		{
			get => _parameterName;
			set
			{
				//prevent stupid null values
				_parameterName = value ?? string.Empty;
			}
		}
		/// <summary>
		/// The possible values of the ParameterName
		/// </summary>
		public List<UrlParameter> Parameters { get; set; }
	}

	/// <summary>
	/// Updates URL parameter values - replace old values to new values
	/// </summary>
	/// <param name="urlString">The URL to update</param>
	/// <param name="parametersToManipulate">List of Values from Old to New</param>
	/// <returns>Modified URL which is generated based on the parameter groups</returns>
	public static string UpdateUrl(string urlString, params UrlParameterGroup[] parametersToManipulate)
	{
		try
		{
			Uri uri = new Uri(urlString);
			var parameters = HttpUtility.ParseQueryString(uri.Query);
			if (parameters != null)
			{
				foreach (var parameterGroup in parametersToManipulate)
				{
					var parameterValue = parameters.Get(parameterGroup.ParameterName);
					if (!string.IsNullOrEmpty(parameterValue))
					{
						var cleanGroup = parameterGroup.Parameters.Where(x => !string.IsNullOrEmpty(x.FromValue)).ToList();
						foreach (var parameter in parameterGroup.Parameters)
							parameterValue = parameterValue.Replace(parameter.FromValue, parameter.ToValue);
					}
					parameters.Set(parameterGroup.ParameterName, parameterValue);
					urlString = new UriBuilder(uri) { Query = parameters.ToString() }.Uri.ToString();
				}
			}
		}
		catch
		{
			//just in case an unknown error pops out just ignore and proceed
		}
		return urlString;
	}
}
