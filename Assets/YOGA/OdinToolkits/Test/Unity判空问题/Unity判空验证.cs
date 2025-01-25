using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Unity判空问题
{
    public class Unity判空验证 : MonoBehaviour
    {
        /*
         - 总结
         - 使用 `==` 运算符可以检查对象是否已被销毁，这在某些情况下非常有用
         - `==` 运算符也可以用来检查两个 `UnityEngine.Object` 实例是否相同
         - 在某些情况下，使用 `==` 运算符可能比 `is null` 或 `ReferenceEquals` 更快。
         - 使用 `==` 运算符可以确保你的代码与 Unity 编辑器的预期行为保持一致。
         - 如果你需要确保对象的引用确实为 `null`，或者你正在处理非 `UnityEngine.Object` 类型的对象，那么使用 `is null` 或 `ReferenceEquals` 是更安全的选择。
         */
        public int 输出次数;
        public GameObject obj;

        void Start()
        {
            obj = new GameObject();
            Destroy(obj);
            Log(obj);
            输出次数++;
        }

        void Update()
        {
            if (输出次数 < 2)
            {
                Log(obj);
                输出次数++;
            }
        }

        [Button("输出字段测试")]
        void Log1()
        {
            Log(obj);
        }

        void Log(GameObject go)
        {
            Debug.Log("开始输出，当前次数为: " + 输出次数);
            Debug.Log("go == null 的结果为: ");
            Debug.Log(go == null);
            // 销毁物体后，字段引用丢失，此时丢失引用的状态在 C# 中不算为 null
            Debug.Log("go is null 的结果为: ");
            Debug.Log(go is null);
            Debug.Log("ReferenceEquals(go, null) 的结果为: ");
            Debug.Log(ReferenceEquals(go, null));
            Debug.Log("结束输出");
        }
    }
}