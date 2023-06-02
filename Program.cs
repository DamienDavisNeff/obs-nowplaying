using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.Media.Control;
using WindowsMediaController;
using System.Diagnostics;

namespace Sample.CMD
{
    class Program
    {
        static MediaManager? mediaManager;
        static object _writeLock = new object();

        public static void Main()
        {

            Console.Title = "Now Playing";

            mediaManager = new MediaManager();

            mediaManager.OnAnySessionOpened += MediaManager_OnAnySessionOpened;
            mediaManager.OnAnySessionClosed += MediaManager_OnAnySessionClosed;
            mediaManager.OnFocusedSessionChanged += MediaManager_OnFocusedSessionChanged;
            mediaManager.OnAnyPlaybackStateChanged += MediaManager_OnAnyPlaybackStateChanged;
            mediaManager.OnAnyMediaPropertyChanged += MediaManager_OnAnyMediaPropertyChanged;

            mediaManager.Start();

            Console.ReadLine();
            mediaManager.Dispose();
        }

        private static void MediaManager_OnAnySessionOpened(MediaManager.MediaSession session)
        {
            WriteLineColor("-- New Source: " + session.Id, ConsoleColor.Green);
        }
        private static void MediaManager_OnAnySessionClosed(MediaManager.MediaSession session)
        {
            WriteLineColor("-- Removed Source: " + session.Id, ConsoleColor.Red);
        }

        private static void MediaManager_OnFocusedSessionChanged(MediaManager.MediaSession mediaSession)
        {
            WriteLineColor("== Session Focus Changed: " + mediaSession?.ControlSession?.SourceAppUserModelId, ConsoleColor.Gray);
        }

        private static void MediaManager_OnAnyPlaybackStateChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionPlaybackInfo args)
        {
            WriteLineColor($"{sender.Id} is now {args.PlaybackStatus} - {args.PlaybackType }", ConsoleColor.Yellow);
            WriteLineColor($"[DBG] {args}");
        }

        private static async void MediaManager_OnAnyMediaPropertyChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionMediaProperties args)
        {
            WriteLineColor($"{sender.Id} is now playing {args.Title} {(String.IsNullOrEmpty(args.Artist) ? "" : $"by {args.Artist}")}", ConsoleColor.Cyan);
            SaveFileText($"{args.Title}","title");
            SaveFileText($"{args.Artist}","artist");
            cts.Cancel();
            cts = new CancellationTokenSource();
            await RandomAccess_SaveImage(args.Thumbnail,"album",cts.Token);
        }

        public static void WriteLineColor(object toprint, ConsoleColor color = ConsoleColor.White)
        {
            lock (_writeLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + toprint);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void SaveFileText(string towrite, string filename, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter($"{filename}.txt",append)) {
                    writer.WriteLine($"{towrite}");
            }
        }
        
        static CancellationTokenSource cts = new CancellationTokenSource();
        public static async Task RandomAccess_SaveImage(IRandomAccessStreamReference streamReference, string filename, CancellationToken cancellationToken, bool overwrite = true)
        {

            while(!cancellationToken.IsCancellationRequested) {

                WriteLineColor("[WARN] Image saving is a work-in-progress & is Debug Only",ConsoleColor.DarkYellow);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while(await IsFileLocked($"{Directory.GetCurrentDirectory()}\\{filename}.png") && stopwatch.Elapsed.TotalMilliseconds < 4000) {
                    await Task.Delay(300);
                }
                if(stopwatch.Elapsed.TotalMilliseconds >= 4000) {
                    stopwatch.Stop();
                    return;
                }
                stopwatch.Stop();

                if(File.Exists($"{Directory.GetCurrentDirectory()}\\{filename}.png")) {
                    try {
                        File.Delete($"{Directory.GetCurrentDirectory()}\\{filename}.png");
                    } catch (IOException ex) {
                        WriteLineColor($"[ERROR] {ex}",ConsoleColor.Red);
                        cts.Cancel();
                        cts = new CancellationTokenSource();
                        return;
                    }
                }

                stopwatch = new Stopwatch();
                stopwatch.Start();
                while(await IsFileLocked($"{Directory.GetCurrentDirectory()}\\{filename}.png") && stopwatch.Elapsed.TotalMilliseconds < 4000) {
                    await Task.Delay(300);
                }
                if(+stopwatch.Elapsed.TotalMilliseconds >= 4000) {
                    stopwatch.Stop();
                    cts.Cancel();
                    cts = new CancellationTokenSource();
                    return;
                }

                if(streamReference == null) {
                    try {
                        await SavePlaceholder($"https://via.placeholder.com/{140}",$"{Directory.GetCurrentDirectory()}\\{filename}.png");
                    } catch (IOException ex) {
                        WriteLineColor($"[ERROR] {ex}",ConsoleColor.Red);
                        cts.Cancel();
                        cts = new CancellationTokenSource();
                        return;
                    }
                    break;
                } else {
                    try {
                        using (var stream = await streamReference.OpenReadAsync()) {
                            var decoder = await BitmapDecoder.CreateAsync(stream);
                            var pixelData = await decoder.GetPixelDataAsync();
                            var outputFile = new FileInfo($"{Directory.GetCurrentDirectory()}\\{filename}.png");
                            using (var fileStream = outputFile.Open(FileMode.Create)) {
                                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream.AsRandomAccessStream());
                                encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, decoder.PixelWidth, decoder.PixelHeight, decoder.DpiX, decoder.DpiY, pixelData.DetachPixelData());
                                await encoder.FlushAsync();
                                await fileStream.FlushAsync();
                            }
                        }
                    } catch (IOException ex) {
                        WriteLineColor($"[ERROR] {ex}",ConsoleColor.Red);
                        cts.Cancel();
                        cts = new CancellationTokenSource();
                        return;
                    }
                    
                }

                WriteLineColor("[COMPLETE] Image Save Complete", ConsoleColor.Green);

                cts.Cancel();
                cts = new CancellationTokenSource();

            }

            
            
        }

        public static async Task SavePlaceholder(string url, string filename)
        {
            using (var client = new HttpClient())
            using (var stream = await client.GetStreamAsync(url))
            using (var fileStream = new FileStream(filename, FileMode.Create))
            await stream.CopyToAsync(fileStream);
        }

        public static async Task<bool> IsFileLocked(string filename) {
            if(!File.Exists($"{filename}")) return false;
            try {
                using(FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) {
                    stream.Close();
                    await Task.Delay(100);
                    return false;
                }
            } catch (IOException ex) { 
                if(ex is FileNotFoundException || ex is DirectoryNotFoundException) return false;
                return true; 
            }
        }

        
    }
}