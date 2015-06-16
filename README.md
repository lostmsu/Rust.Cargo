# Rust.Cargo
CLR-based library for Rust package manager and build system Cargo

# Building
This project depends on HyperTomlProcessor portable library, which I am also working on. Since Rust.Cargo is in its early stages (and so is portable HyperTomlProcessor), NuGet is not yet used between them. So to build succesfully, proper directory structure should be maintained:

    sourceRoot\
    - OSS\
      - HyperTomlProcessor\ (branch features/portable from https://github.com/lostmsu/HyperTomlProcessor/ )
    - Rust\
      - Cargo\ (this project)

Once you have this folder structure, and proper branches, you should be able to build Lost.Rust.Cargo.sln
