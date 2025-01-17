﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Debugger.IDE.Intellisense;

namespace Debugger.IDE.Activity {

    /// <summary>
    /// Builds the TypeInfo database either when started
    /// or when the filesystem watcher detects a change
    /// 
    /// The typesystem is considered to be volatile, and may be there one minute, and missing the next
    /// </summary>
    public class IDBBuilderActivity {
        static FileSystemWatcher watcher_;

        public static void BuildIntellisenseDatabase() {
            
            string dir = System.Reflection.Assembly.GetEntryAssembly().Location;
            dir = System.IO.Path.GetDirectoryName(dir);
            dir = System.IO.Path.Combine(dir, "bin");
            string file = System.IO.Path.Combine(dir, "dump.h");

            // For convenience purposes check for the old file and auto update it to the new "AngelScriptAPI.h" naming
            // This change removes the silly need to rename the API header
            string newFile = System.IO.Path.Combine(dir, "AngelScriptAPI.h");
            if (System.IO.File.Exists(file) && !System.IO.File.Exists(newFile))
                System.IO.File.Copy(file, newFile);
            
            if (watcher_ == null && System.IO.Directory.Exists(dir)) {
                try
                {
                    watcher_ = new FileSystemWatcher(dir);
                    watcher_.Changed += watcher__Changed;
                    watcher_.EnableRaisingEvents = true;
                } 
                catch (Exception)
                {
                    // UAC and other user/system issues may deny us access
                }
            }

            Thread thread = new Thread(delegate() {
                Globals globs = new Globals(true);
                DumpParser parser = new DumpParser();
                try
                {
                    StringReader rdr = new StringReader(File.ReadAllText(newFile));
                    parser.ParseDumpFile(rdr, globs);
                    MainWindow.inst().Dispatcher.Invoke(delegate()
                    {
                        IDEProject.inst().GlobalTypes = globs;
                    });
                } 
                catch (Exception)
                {
                    // swallow all exceptions
                }
            });
            thread.Start();

        }

        static void watcher__Changed(object sender, FileSystemEventArgs e) {
            BuildIntellisenseDatabase();
        }
    }
}
