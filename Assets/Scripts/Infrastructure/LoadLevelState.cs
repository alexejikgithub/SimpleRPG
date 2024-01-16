using CodeBase.Logic;
using SimpleRPG.CameraLogic;
using UnityEngine;

namespace SimpleRPG.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string HeroPath = "Hero/Hero";
        private const string UIPath = "UI/Hud";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }


        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject hero = Instantiate(HeroPath, initialPoint.transform.position);
            Instantiate(UIPath);
            StartCameraFollow(hero);
            _stateMachine.Enter<GameLoopState>();
        }

        private void StartCameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}