using System;
using Sexy;
using System.IO.Compression;
using System.Windows.Forms;

namespace DotNETPvZ
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            const string PackFileName = "data.zip";
#if !DEBUG
            try
            {
#endif

                var game = new Main();
#if !DEBUG
                //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ZipFile.ExtractToDirectory(PackFileName, AppDomain.CurrentDomain.BaseDirectory, true);
#endif
            game.Window.Title = String.Format
            ("Plants VS Zombies NET", Lawn.LawnApp.AppVersionNumber);
            game.Run();
#if !DEBUG
            }
            catch (NullReferenceException err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show(String.Format("Please put data.zip in the execution directory", PackFileName), "Resource pack not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException err)
            {
                MessageBox.Show("Error: Internal resource not found\n" + ((err.InnerException == null) ? err.Message : err.InnerException.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
    }
}
