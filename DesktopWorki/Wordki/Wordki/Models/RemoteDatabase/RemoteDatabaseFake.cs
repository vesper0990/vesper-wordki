﻿using Wordki.Helpers.Command;

namespace Wordki.Models.RemoteDatabase {
  public class RemoteDatabaseFake : RemoteDatabaseBase {
    public override CommandQueue<ICommand> GetDownloadQueue() {
      return new CommandQueue<ICommand>();
    }

    public override CommandQueue<ICommand> GetUploadQueue() {
      return new CommandQueue<ICommand>();
    }
  }
}
