using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DllAnd
{
    public static class BitwiseAnd
    {
        public static void ASMAnd()
        {
            AppDomain asmDomain = Thread.GetDomain();
            AssemblyBuilder asmBuilder = asmDomain.DefineDynamicAssembly(new AssemblyName("AND"), AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder mullModule = asmBuilder.DefineDynamicModule("logAnd.dll", true);
            TypeBuilder typeBuilder = mullModule.DefineType("AND", TypeAttributes.Public);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("AND", MethodAttributes.Public, typeof(int), new Type[] { typeof(int), typeof(int) });
            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ldarg_2);
            iLGenerator.Emit(OpCodes.And);
            iLGenerator.Emit(OpCodes.Ret);
            typeBuilder.CreateType();
            asmBuilder.Save("logAnd.dll");
        }

    }
}
