using AVG_Command;
using AVG_Command_Creator;
using AVG_module;
using System.Collections.Generic;

public class CommandSender : MonoSingleton<CommandSender>
    {
        public CommandConfig commandConfig;
        private Queue<CommandBase> commandQueue { get; set; }
        private CommandBase currentCommand = null;
        private bool isExecuted = false;


        public void Init()
        {
            AVGModule.Instance.plotBegin.AddListener(() => { PlotBegin(); });

            AVGModule.Instance.plotEnd.AddListener(() => { PlotEnd(); });
        }

        public void PlotBegin()
        {
            if (commandQueue != null || currentCommand != null) return;

            commandQueue = this.GetCommandsQueue(this.commandConfig);
            currentCommand = commandQueue.Dequeue();
            isExecuted = false;
            AvgUISettings.Init();
            
        }

        public void PlotEnd()
        {
            AvgUISettings.Instance.End();
            Destroy(gameObject);
        }

        protected override void OnStart()
        {
        }

        private void Update()
        {
            if (commandQueue == null || currentCommand == null) return;

            if (!isExecuted)
            {
                currentCommand.Execute();
                isExecuted = true;
            }

            currentCommand.OnUpdate();

            if (currentCommand.IsFinished())
            {
                if (commandQueue.Count == 0)
                {
                    currentCommand = null;
                }
                else
                {
                    currentCommand = commandQueue.Dequeue();
                }

                isExecuted = false;
            }
        }

        public Queue<CommandBase> GetCommandsQueue(CommandConfig config)
        {
            Queue<CommandBase> commandsQueue = new Queue<CommandBase>(config.commandList);
            return commandsQueue;
        }

        public int GetCommandsCount()
        {
            return commandQueue.Count;
        }

        public CommandBase PeekCommand()
        {
            return commandQueue.Peek();
        }

        public CommandBase DequeueCommand()
        {
            return commandQueue.Dequeue();
        }
    }
