using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; //
using System.Threading.Tasks;

namespace OS_3k3n3n
{
    /// <summary>
    /// Структура для характеристик процесса
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ProcessEntry32
    {
        const int MAX_PATH = 260;
        public uint dwSize; // размер записи
        public uint cntUsage; // счетчик ссылок процесса
        public uint th32ProcessID; // идентификационный номер процесса
        public IntPtr th32DefaultHeapID; // идентификатор кучи процесса
        public uint th32ModuleID; // идентифицирует модуль связанный с процессом 
        public uint cntThreads; // кол-во потоков в данном процессе
        public uint th32ParentProcessID; // идентификатор родительского процесса
        public int pcPriClassBase; // базовый приоритет процесса 
        public uint dwFlags; // зарезервировано
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)] 
        public string szExeFile; // путь к exe файлу или драйверу связанному с этим процессом

        /// <summary>
        /// Перевод структуры в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";

            str += ("Имя: ") + szExeFile.ToString() + Environment.NewLine +
                "Идентификатор процесса: " + th32ProcessID.ToString() + Environment.NewLine;
            //    "Кол-во потоков процесса: "+cntThreads.ToString() + Environment.NewLine+
            //    "Идентификатор родительского процесса: "+th32ParentProcessID.ToString() + Environment.NewLine;

            /*.Append("dwSize: ").Append(dwSize).Append(" ")
            .Append("cntUsage: ").Append(cntUsage).Append(" ")
            .Append("th32DefaultHeapID: ").Append(th32DefaultHeapID).Append(" ")
            .Append("th32ModuleID: ").Append(th32ModuleID).Append(" ")
            .Append("pcPriClassBase: ").Append(pcPriClassBase).Append(" ")
            .Append("dwFlags: ").Append(dwFlags).Append(" ")*/
            return str;
        }
    }

    /// <summary>
    /// Структура для модуля процесса
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ModuleEntry32
    {
        const int MAX_PATH = 260;
        public uint dwSize;  // размер записи
        public uint th32ModuleID; // идентификатор модуля
        public uint th32ProcessID; // идентификатор процесса
        public uint GlblcntUsage; // глобальный счетчик ссылок
        public uint ProccntUsage; // счетчик ссылок в контексте процессора
        public IntPtr modBaseAddr; //  базовый адресс модуля в памяти
        public uint modBaseSize; // размер модуля памяти
        public IntPtr hModule; // дескриптор модуля
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szModule; // название модуля
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string szExePath; // полный путь модуля

        /// <summary>
        /// Перевод структуры в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";

            str += "Информация о модуле:" + Environment.NewLine +
                "Имя: " + szModule.ToString() + Environment.NewLine +
                "Базовый адрес: " + modBaseAddr.ToString() + Environment.NewLine +
                "Размер памяти: " + modBaseSize.ToString() + " байт" + Environment.NewLine +
                "Путь к файлу: " + szExePath.ToString() + Environment.NewLine;
               // "Кол-во процессов использующих модуль: "+count.ToString() + Environment.NewLine;

            /*.Append("dwSize: ").Append(dwSize).Append(" ")
            .Append("th32ModuleID: ").Append(th32ModuleID).Append(" ")
            .Append("th32ProcessID: ").Append(th32ProcessID).Append(" ")
            .Append("GlblcntUsage: ").Append(GlblcntUsage).Append(" ")
            .Append("ProccntUsage: ").Append(ProccntUsage).Append(" ")                    
            .Append("hModule: ").Append(hModule).Append(" ")
            .Append(Environment.NewLine);*/
            return str;
        }
    }

    internal class ToolHelp32
    {

        [Flags]
        internal enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            Inherit = 0x80000000,
            All = 0x0000001F,
            NoHeaps = 0x40000000
        }

        #region Import from kernel32

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateToolhelp32Snapshot([In]UInt32 dwFlags, [In]UInt32 th32ProcessID);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool Process32First([In]IntPtr hSnapshot, ref ProcessEntry32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool Process32Next([In]IntPtr hSnapshot, ref ProcessEntry32 lppe);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern bool Module32First([In]IntPtr hSnapshot, ref ModuleEntry32 lpme);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern bool Module32Next([In]IntPtr hSnapshot, ref ModuleEntry32 lpme);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle([In] IntPtr hObject);

        #endregion

        /// <summary>
        /// Получение перечисления всех процессов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProcessEntry32> GetProcessList()
        {
            IntPtr handleToSnapshot = IntPtr.Zero;
            try
            {
                ProcessEntry32 procEntry = new ProcessEntry32(); //создаем экземпляр ProcessEntry
                procEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(ProcessEntry32)); //выделяем для него память
                handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.All, 0); //создаем снимок оперативной памяти

                if (Process32First(handleToSnapshot, ref procEntry)) //если есть хотя бы один процесс
                {
                    do
                    {
                        yield return procEntry; //возвращаем процесс
                    } while (Process32Next(handleToSnapshot, ref procEntry)); //пока таковые не закончатся
                }
                else //если не удалось получить ни один процесс, кидаем исключение
                {
                    throw new ApplicationException("Failed with win32 error code {0}" + Marshal.GetLastWin32Error().ToString());
                }
            }
            finally
            {
                CloseHandle(handleToSnapshot); //уничтожаем снимок
            }
        }

        /// <summary>
        /// Получение перечисления всех модулей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleEntry32> GetModuleList()
        {
            IntPtr handleToSnapshot = IntPtr.Zero;
            try
            {
                ModuleEntry32 modEntry = new ModuleEntry32(); //создаем экземпляр структуры-модуля
                modEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(ModuleEntry32)); //выделяем под него память
                handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.All, 0); //создаем снимок оперативной памяти

                if (Module32First(handleToSnapshot, ref modEntry)) //если есть хотя бы один модуль
                {
                    do
                    {
                        yield return modEntry; //поочередно возвращаем их
                    } while (Module32Next(handleToSnapshot, ref modEntry)); //до тех пор, пока не вернем все
                }
                else //если не получили ни одного, выбрасываем исключение
                {
                    throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
                }
            }
            finally
            {
                CloseHandle(handleToSnapshot); //уничтожаем снимок
            }
        }

        /// <summary>
        /// Получение всех модулей, используемых каким-либо процессом
        /// </summary>
        /// <param name="th32ProcessID"></param>
        /// <returns></returns>
        public IEnumerable<ModuleEntry32> GetModuleList(uint th32ProcessID)
        {
            IntPtr handleToSnapshot = IntPtr.Zero;
            try
            {
                ModuleEntry32 modEntry = new ModuleEntry32(); //создаем экземпляр структуры-модуля
                modEntry.dwSize = (UInt32)Marshal.SizeOf(typeof(ModuleEntry32)); //выделяем под него память
                handleToSnapshot = CreateToolhelp32Snapshot((uint)SnapshotFlags.All, th32ProcessID); //создаем снимок заданного процесса

                if (Module32First(handleToSnapshot, ref modEntry)) //получаем первый модуль
                {
                    do
                    {
                        yield return modEntry; //и возвращаем все последующие
                    } while (Module32Next(handleToSnapshot, ref modEntry)); // пока такие остались
                }
                else //если не оказалось ни одного, ничего не возвращаем
                {
                    yield break;
                }
            }
            finally
            {
                CloseHandle(handleToSnapshot); //закрываем снимок
            }
        }

        #region Task
        /// <summary>
        /// Получает перечисление всех процессов в диапазоне от From до To
        /// </summary>
        /// <param name="from">левая граница диапазона</param>
        /// <param name="to">Правая граница диапазона</param>
        /// <returns>Перечисление</returns>
        public IEnumerable<ProcessEntry32> GetProcessesInIdRange(int from, int to)
        {
            foreach (ProcessEntry32 pe in this.GetProcessList())
                if (pe.th32ProcessID >= from && pe.th32ProcessID <= to)
                    yield return pe;
        }

        /// <summary>
        /// Считает количество модулей, с которыми работает заданный процесс
        /// </summary>
        /// <param name="processId">ID процесса</param>
        /// <returns>Количество модулей</returns>
        public int CountModuleLength(uint processId)
        {
            int res=0;
            foreach (ModuleEntry32 me in this.GetModuleList(processId))
                ++res;
            return res;
        }

        /// <summary>
        /// Максимальная длина модулей всех процессов в заданном диапазоне
        /// </summary>
        /// <param name="from">Левая граница диапазона</param>
        /// <param name="to">Правая граница диапазона</param>
        /// <returns>Максимальная длина</returns>
        public int MaxModuleLength (int from, int to)
        {
            int max = 0; //Максимальная длина
            int curr = 0; //текущая длина
            foreach (ProcessEntry32 pe in this.GetProcessesInIdRange(from,to))
            {
                curr = this.CountModuleLength(pe.th32ProcessID);
                if (curr > max)
                    max = curr;
            }
            return max;
        }

        /// <summary>
        /// Перечисление всех процессов с заданной длиной модуля
        /// </summary>
        /// <param name="from">Левая граница диапазона</param>
        /// <param name="to">Правая граница диапазона</param>
        /// <returns>Перчеисление всех процессов с максимальной длиной</returns>
        public IEnumerable<ProcessEntry32> GetProccessesMaxLenModsInRange(int from, int to, int len)
        {
            foreach (ProcessEntry32 pe in this.GetProcessesInIdRange(from, to))
                if (CountModuleLength(pe.th32ProcessID) == len)
                    yield return pe;
        }





        /// <summary>
        /// Считает количество модулей, с которыми работает заданный процесс
        /// </summary>
        /// <param name="processId">ID процесса</param>
        /// <returns>Количество модулей</returns>
        public long CountModuleMemSize(uint processId)
        {
            long res = 0;
            foreach (ModuleEntry32 me in this.GetModuleList(processId))
                res +=me.modBaseSize;
            return res;
        }

        /// <summary>
        /// Максимальная длина модулей всех процессов в заданном диапазоне
        /// </summary>
        /// <param name="from">Левая граница диапазона</param>
        /// <param name="to">Правая граница диапазона</param>
        /// <returns>Максимальная длина</returns>
        public long MaxModuleMemSize(int from, int to)
        {
            long max = 0; //Максимальная длина
            long curr = 0; //текущая длина
            foreach (ProcessEntry32 pe in this.GetProcessesInIdRange(from, to))
            {
                curr = this.CountModuleMemSize(pe.th32ProcessID);
                if (curr > max)
                    max = curr;
            }
            return max;
        }

        /// <summary>
        /// Перечисление всех процессов с заданной длиной модуля
        /// </summary>
        /// <param name="from">Левая граница диапазона</param>
        /// <param name="to">Правая граница диапазона</param>
        /// <returns>Перчеисление всех процессов с максимальной длиной</returns>
        public IEnumerable<ProcessEntry32> GetProccessesMaxMemSizeModsInRange(int from, int to, long len)
        {
            foreach (ProcessEntry32 pe in this.GetProcessesInIdRange(from, to))
                if (CountModuleMemSize(pe.th32ProcessID) == len)
                    yield return pe;
        }

        #endregion

    }
}
