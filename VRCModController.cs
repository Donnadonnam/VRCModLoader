﻿using System;
using System.Reflection;

namespace VRCModLoader
{
    internal class VRCModController
    {
        private MethodInfo onApplicationStartMethod;
        private MethodInfo onApplicationQuitMethod;
        private MethodInfo onLevelWasLoadedMethod;
        private MethodInfo onLevelWasInitializedMethod;
        private MethodInfo onUpdateMethod;
        private MethodInfo onFixedUpdateMethod;
        private MethodInfo onLateUpdateMethod;
        private MethodInfo onGUIMethod;
        private MethodInfo onModSettingsApplied;
        public VRCMod mod;

        public VRCModController(VRCMod mod)
        {
            this.mod = mod;

            MethodInfo[] methods = mod.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo method in methods)
            {
                if (method.Name.Equals("OnApplicationStart") && method.GetParameters().Length == 0)
                    onApplicationStartMethod = method;
                if (method.Name.Equals("OnApplicationQuit") && method.GetParameters().Length == 0)
                    onApplicationQuitMethod = method;
                if (method.Name.Equals("OnLevelWasLoaded") && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(int))
                    onLevelWasLoadedMethod = method;
                if (method.Name.Equals("OnLevelWasInitialized") && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(int))
                    onLevelWasInitializedMethod = method;
                if (method.Name.Equals("OnUpdate") && method.GetParameters().Length == 0)
                    onUpdateMethod = method;
                else if (method.Name.Equals("Update") && method.GetParameters().Length == 0 && onUpdateMethod == null)
                    onUpdateMethod = method;
                if (method.Name.Equals("OnFixedUpdate") && method.GetParameters().Length == 0)
                    onFixedUpdateMethod = method;
                else if (method.Name.Equals("FixedUpdate") && method.GetParameters().Length == 0 && onFixedUpdateMethod == null)
                    onFixedUpdateMethod = method;
                if (method.Name.Equals("OnLateUpdate") && method.GetParameters().Length == 0)
                    onLateUpdateMethod = method;
                else if (method.Name.Equals("LateUpdate") && method.GetParameters().Length == 0 && onLateUpdateMethod == null)
                    onLateUpdateMethod = method;
                if (method.Name.Equals("OnGUI") && method.GetParameters().Length == 0)
                    onGUIMethod = method;
                if (method.Name.Equals("OnModSettingsApplied") && method.GetParameters().Length == 0)
                    onModSettingsApplied = method;
            }
        }

        public virtual void OnApplicationStart() => onApplicationStartMethod?.Invoke(mod, new object[] { });
        public virtual void OnApplicationQuit() => onApplicationQuitMethod?.Invoke(mod, new object[] { });
        [Obsolete("Use 'UnityEngine.SceneManagement.SceneManager.sceneLoaded'")]
        public virtual void OnLevelWasLoaded(int level) => onLevelWasLoadedMethod?.Invoke(mod, new object[] { level });
        public virtual void OnLevelWasInitialized(int level) => onLevelWasInitializedMethod?.Invoke(mod, new object[] { level });
        public virtual void OnUpdate() => onUpdateMethod?.Invoke(mod, new object[] { });
        public virtual void OnFixedUpdate() => onFixedUpdateMethod?.Invoke(mod, new object[] { });
        public virtual void OnLateUpdate() => onLateUpdateMethod?.Invoke(mod, new object[] { });
        public virtual void OnGUI() => onGUIMethod?.Invoke(mod, new object[] { });
        public virtual void OnModSettingsApplied() => onModSettingsApplied?.Invoke(mod, new object[] { });
    }
}