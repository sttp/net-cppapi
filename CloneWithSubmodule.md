# Cloning and updating the C++ submodule

Run the following commands in a terminal. The C++ repository is located at `src/lib/cppapi` inside `net-cppapi`.

## Clone the repository and its recorded submodule version

```bash
git clone --recurse-submodules https://github.com/sttp/net-cppapi.git
cd net-cppapi
```

This checks out the exact C++ commit recorded by `net-cppapi`. A detached HEAD inside the submodule is normal; checking out a branch is not required to build it.

For a clone without initialized submodules, run this from the `net-cppapi` root:

```bash
git submodule update --init --recursive
```

## Update an existing clone to the recorded version

To pull updates to `net-cppapi` and check out the C++ commit they specify, run from the `net-cppapi` root:

```bash
git pull --ff-only
git submodule update --init --recursive
```

This follows the version recorded by `net-cppapi`, which may differ from the latest C++ release or branch tip.

## Update the C++ submodule to the latest code

First commit or stash any local changes inside the submodule. From the `net-cppapi` root:

```bash
git submodule update --init --recursive
git -C src/lib/cppapi fetch origin --tags
git -C src/lib/cppapi switch main
git -C src/lib/cppapi pull --ff-only origin main
```

The C++ repository's primary branch is `main`. If a local branch has diverged, `--ff-only` stops instead of creating a merge; inspect the local commits before proceeding.

### Select a specific release instead

For a reproducible release update, check out the desired tag instead of following `main`. For example, from the `net-cppapi` root:

```bash
git submodule update --init --recursive
git -C src/lib/cppapi fetch origin --tags
git -C src/lib/cppapi switch --detach v1.1.3
```

## Record the updated submodule commit

After updating the C++ checkout, follow the [build instructions](src/lib/README.md) to rebuild and test the native and managed wrappers together. Update `src/lib/sttp.i` and regenerate SWIG bindings when the native API changes. The wrapper targets .NET 6, .NET 9, and .NET 10, and native builds use Boost 1.92.0.

Then review and commit the new submodule reference from the `net-cppapi` root:

```bash
git diff --submodule=log -- src/lib/cppapi
git add src/lib/cppapi
git commit -m "Update C++ submodule to v1.1.3"
git push
```

Adjust the commit message for the version selected, and include any required wrapper changes in the same update. The parent repository records the submodule's commit ID, not its branch name. Pushing the parent repository does not push changes made inside the C++ repository; any custom C++ commit must be pushed there first so other users can fetch it.
