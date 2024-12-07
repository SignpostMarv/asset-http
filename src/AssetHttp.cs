using System.IO;
using System.Linq;
using System.Net;
using SkiaSharp;
using CUE4Parse.UE4.Versions;

namespace SatisfactorDotDev.AssetHttp;

class AssetHttp
{
	private static HttpListener? listener = null;
	private static Dictionary<string, Games.Satisfactory> Satisfactory = new Dictionary<string, Games.Satisfactory>();


	public static void Main()
	{
		listener = new HttpListener();

		listener.Prefixes.Add($"http://localhost:5000/");

		Games.Satisfactory[] versions = {
			new Games.Satisfactory("0.8.3.3", EGame.GAME_UE5_2),
			new Games.Satisfactory("1.0.0.7", EGame.GAME_UE5_3),
			new Games.Satisfactory("1.0.1.0", EGame.GAME_UE5_3),
			new Games.Satisfactory("1.0.1.1", EGame.GAME_UE5_3),
			new Games.Satisfactory("1.0.1.2", EGame.GAME_UE5_3),
		};

		foreach (Games.Satisfactory version in versions)
		{
			Satisfactory[version.game_version] = version;
			Console.WriteLine($"Satisfactory {version.game_version} {(version.Exists ? "does" : "does not")} exist");
		}

		listener.Start();

		while(true) {
			HttpListenerContext context = listener.GetContext();

			UriToAsset(context);
		}

		listener.Stop();
	}

	protected static void UriToAsset(HttpListenerContext context)
	{
		Uri Url = context.Request.Url;

		if (!Url.LocalPath.StartsWith("/satisfactory/")) {
			return;
		}

		string[] parts = Url.LocalPath.Split("/", 4);

		if (
			parts.Length < 4
			|| !(Satisfactory.ContainsKey(parts[2]))
		) {
			return;
		}

		object files = Satisfactory[parts[2]].Files;

		SKBitmap texture = Satisfactory[parts[2]].LoadTexture($"/{parts[3]}");

		SKData png = texture.Encode(SKEncodedImageFormat.Png, 100);

		context.Response.ContentLength64 = png.Size;
		png.AsStream().CopyTo(context.Response.OutputStream);
		context.Response.OutputStream.Close();
	}
}
